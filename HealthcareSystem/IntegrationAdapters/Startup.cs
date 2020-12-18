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
using Repository;
using System;
using IntegrationAdapters.Services;
using System.Collections.Generic;

namespace IntegrationAdapters
{
    public class Startup
    {

        public static IConfiguration Configuration { get; set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            IConfiguration conf = Configuration.GetSection("DbConnectionSettings");
            DbConnectionSettings dbSettings = conf.Get<DbConnectionSettings>();

            services.AddControllers();
            services.AddDbContext<MyDbContext>(options =>
            {
                options.UseMySql(
                    dbSettings.ConnectionString,
                    x => x.MigrationsAssembly("Backend").EnableRetryOnFailure(
                        dbSettings.RetryCount, new TimeSpan(0, 0, 0, dbSettings.RetryWaitInSeconds), new List<int>())
                    ).UseLazyLoadingProxies();
            });

            services.AddControllersWithViews();

            services.Configure<RabbitMqConfiguration>(Configuration.GetSection("RabbitMq"));
            services.Configure<SftpConfig>(Configuration.GetSection("SftpConfig"));
            services.AddSingleton<RabbitMqActionBenefitMessageingService>();
            services.AddSingleton<IHostedService, RabbitMqActionBenefitMessageingService>(ServiceProvider => ServiceProvider.GetService<RabbitMqActionBenefitMessageingService>());

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

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddHttpClient();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, MyDbContext context, IAntiforgery antiforgery)
        {
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
