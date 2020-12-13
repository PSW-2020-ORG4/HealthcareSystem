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
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;

namespace IntegrationAdapters
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }

        public IConfiguration Configuration { get; }
        private readonly IWebHostEnvironment _env;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            if (_env.IsDevelopment())
            {
                Console.WriteLine("Configuring for dev.");
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
                int retryCount = Configuration.GetValue<int>("HEROKU_DB_RETRY");
                int retryWait = Configuration.GetValue<int>("HEROKU_DB_RETRY_WAIT");
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

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddHttpClient();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, MyDbContext context)
        {
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
