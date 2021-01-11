using AutoMapper;
using Backend.Communication.SftpCommunicator;
using Backend.Model;
using Backend.Model.Pharmacies;
using Backend.Repository;
using Backend.Repository.DrugConsumptionRepository;
using Backend.Repository.DrugConsumptionRepository.MySqlDrugConsumptionRepository;
using Backend.Service;
using Backend.Service.DrugConsumptionService;
using Backend.Service.Pharmacies;
using Backend.Settings;
using IntegrationAdapters.Adapters;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using IntegrationAdapters.Services;
using System.Collections.Generic;
using Backend.Repository.DrugInRoomRepository;
using Backend.Repository.DrugInRoomRepository.MySqlDrugInRoomRepository;
using Backend.Repository.DrugRepository;
using Backend.Repository.DrugRepository.MySQLDrugRepository;
using Backend.Repository.TenderRepository;
using Backend.Repository.TenderRepository.MySqlTenderRepository;
using Backend.Service.DrugAndTherapy;
using Service.DrugAndTherapy;
using Backend.Communication.RabbitMqConnection;
using System.IO;

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
            services.AddControllers();

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
                int retryCount = Configuration.GetValue<int>("DATABSE_RETRY");
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
            else
            {
                Console.WriteLine("Not dev or test.");
            }

            services.AddControllersWithViews();

            services.Configure<RabbitMqConfiguration>(Configuration.GetSection("RabbitMqSettings"));
            services.Configure<SftpConfig>(Configuration.GetSection("SftpConfig"));
            services.AddSingleton<IRabbitMqConnection, RabbitMqConnection>();
            services.AddSingleton<IRabbitMqTenderingService, RabbitMqTenderingService>();
            services.AddSingleton<IRabbitMqActionBenefitService, RabbitMqActionBenefitService>();

            services.AddScoped<IPharmacyRepo, MySqlPharmacyRepo>();
            services.AddScoped<IPharmacyService, PharmacyService>();
            services.AddScoped<IActionBenefitRepository, MySqlActionBenefitRepository>();
            services.AddScoped<IActionBenefitService, ActionBenefitService>();
            services.AddScoped<ISftpCommunicator, SftpCommunicator>();
            services.AddScoped<IDrugConsumptionRepository, MySqlDrugConsumptionRepository>();
            services.AddScoped<IDrugConsumptionService, DrugConsumptionService>();
            services.AddScoped<IAdapterContext, AdapterContext>();
            services.AddScoped<IActivePatientRepository, MySqlActivePatientRepository>();
            services.AddScoped<IPatientService, PatientService>();
            services.AddScoped<IPushNotificationService, PushNotificationService>();
            services.AddScoped<IConfirmedDrugRepository, MySqlConfirmedDrugRepository>();
            services.AddScoped<IUnconfirmedDrugRepository, MySqlUnconfirmedDrugRepository>();
            services.AddScoped<IDrugInRoomRepository, MySqlDrugInRoomRepository>();
            services.AddScoped<IDrugService, DrugService>();
            services.AddScoped<ITenderRepository, MySqlTenderRepository>();
            services.AddScoped<ITenderService, TenderService>();
            services.AddScoped<ITenderMessageRepository, MySqlTenderMessageRepository>();
            services.AddScoped<ITenderMessageService, TenderMessageService>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddHttpClient();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IAntiforgery antiforgery)
        {
            app.ApplicationServices.GetService<IRabbitMqTenderingService>();
            app.ApplicationServices.GetService<IRabbitMqActionBenefitService>();

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
                            Url = "http://localhost:8383",
                            GrpcHost = "http://localhost",
                            GrpcPort = 9090,
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
