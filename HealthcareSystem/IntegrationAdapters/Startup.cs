using AutoMapper;
using Backend.Communication.SftpCommunicator;
using Backend.Model;
using Backend.Model.Pharmacies;
using Backend.Settings;
using IntegrationAdapters.Adapters;
using IntegrationAdapters.MicroserviceComunicator;
using IntegrationAdapters.Services;
using IntegrationAdapters.Settings;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;

namespace IntegrationAdapters
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }

        public static IConfiguration Configuration { get; set; }
        private readonly IWebHostEnvironment _env;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            if (_env.IsDevelopment() || _env.IsProduction())
            {
                Console.WriteLine("Configuring for " + _env.EnvironmentName + ".");
                IConfiguration conf = Configuration.GetSection("DbConnectionSettings");
                DbConnectionSettings dbSettings = conf.Get<DbConnectionSettings>();

                Console.WriteLine(dbSettings.ConnectionString);

                services.AddDbContext<MyDbContext>(options =>
                {
                    options.UseMySql(
                        dbSettings.ConnectionString,
                        x => x.MigrationsAssembly("Backend").EnableRetryOnFailure(
                            dbSettings.RetryCount, new TimeSpan(0, 0, 0, dbSettings.RetryWaitInSeconds), new List<int>())
                        ).UseLazyLoadingProxies();
                });
            }
            else if (_env.EnvironmentName.ToLower().Equals("test"))
            {
                Console.WriteLine("Configuring for test.");
                int retryCount = Configuration.GetValue<int>("DATABASE_RETRY");
                int retryWait = Configuration.GetValue<int>("DATABASE_RETRY_WAIT");
                string dbURL = Configuration.GetValue<string>("DATABASE_URL");
                DbConnectionSettings dbSettings = new DbConnectionSettings(dbURL, retryCount, retryWait);

                Console.WriteLine(dbSettings.ConnectionString);

                services.AddDbContext<MyDbContext>(options =>
                {
                    options.UseNpgsql(
                        dbSettings.ConnectionString,
                        x => x.MigrationsAssembly("Backend").EnableRetryOnFailure(
                            dbSettings.RetryCount, new TimeSpan(0, 0, 0, dbSettings.RetryWaitInSeconds), new List<string>())
                        ).UseLazyLoadingProxies();
                });
            }
            else if (_env.EnvironmentName.ToLower().Equals("production-multi"))
            {
                Console.WriteLine("Configuring for " + _env.EnvironmentName + ".");
                IConfiguration conf = Configuration.GetSection("DbConnectionSettings");
                DbConnectionSettings dbSettings = conf.Get<DbConnectionSettings>();

                Console.WriteLine(dbSettings.ConnectionString);

                services.AddDbContext<MyDbContext>(options =>
                {
                    options.UseMySql(
                        dbSettings.ConnectionString,
                        x => x.MigrationsAssembly("Backend").EnableRetryOnFailure(
                            dbSettings.RetryCount, new TimeSpan(0, 0, 0, dbSettings.RetryWaitInSeconds), new List<int>())
                        ).UseLazyLoadingProxies();
                });
            }
            else
            {
                Console.WriteLine("Not dev or test.");
            }

            services.AddControllers();
            services.AddControllersWithViews();

            services.Configure<SftpConfig>(Configuration.GetSection("SftpConfig"));
            services.Configure<ServiceSettings>(GetServiceSettings);
            services.AddScoped<ISftpCommunicator, SftpCommunicator>();
            services.AddScoped<IAdapterContext, AdapterContext>();
            services.AddScoped<IPushNotificationService, PushNotificationService>();
            services.AddScoped<IPharmacySystemService, PharmacySystemService>();
            services.AddScoped<IActionBenefitService, ActionBenefitService>();
            services.AddScoped<IPrescriptionService, PrescriptionService>();
            services.AddScoped<IDrugService, DrugService>();
            services.AddScoped<ITenderService, TenderService>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddHttpClient();
        }

        private void GetServiceSettings(ServiceSettings conf)
        {
            conf.TenderServiceUrl = Configuration.GetValue<string>("TENDER_SERVICE_URL");
            conf.ActionBenefitServiceUrl = Configuration.GetValue<string>("ACTION_BENEFIT_SERVICE_URL");
            conf.DrugServiceUrl = Configuration.GetValue<string>("DRUG_SERVICE_URL");
            conf.PharmacySystemServiceUrl = Configuration.GetValue<string>("PHARMACY_SYSTEM_SERVICE_URL");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IAntiforgery antiforgery, IHttpClientFactory httpClientFactory)

        {
            var client = httpClientFactory.CreateClient();

            if (!env.EnvironmentName.ToLower().Equals("test"))
            {
                client.GetAsync("http://localhost:5001/ping");
                client.GetAsync("http://localhost:5002/ping");
                client.GetAsync("http://localhost:5003/ping");
                client.GetAsync("http://localhost:5004/ping");
            }

            app.Use(next => context =>
            {
                string path = context.Request.Path.Value;

                if (
                    string.Equals(path, "/", StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(path, "/index.html", StringComparison.OrdinalIgnoreCase))
                {
                    context.Response.Cookies.Append("VapidPublicKey", Configuration.GetSection("VapidKeys")["PublicKey"], new CookieOptions() { HttpOnly = false });
                }

                return next(context);
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            if (env.EnvironmentName.ToLower().Equals("test"))
                using (var scope = app.ApplicationServices.CreateScope())
                using (var context = scope.ServiceProvider.GetService<MyDbContext>())
                {
                    try
                    {
                        Console.WriteLine("Seeding ISA Pharmacy System for Test env.");
                        context.Pharmacies.Add(new PharmacySystem()
                        {
                            Name = "ISA Pharmacy",
                            Url = "http://isabackend:8080",
                            GrpcAdress = new GrpcAdress("http://isabackend", 9090),
                            ActionsBenefitsSubscribed = true,
                            ActionsBenefitsExchangeName = "exchange",
                            ApiKey = "apikey"
                        });
                        context.SaveChanges();
                        Console.WriteLine("Seeding finished.");
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Seeding failed.");
                    }
                }

            if (!Directory.Exists("Resources"))
            {
                Directory.CreateDirectory("Resources");
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
