using Backend.Communication.RabbitMqConfuguration;
using Backend.Communication.RabbitMqConnection;
using Backend.Model;
using Backend.Settings;
using IATenderService.Settings;
using IntegrationAdaptersTenderService.Repository;
using IntegrationAdaptersTenderService.Repository.Implementation;
using IntegrationAdaptersTenderService.Service;
using IntegrationAdaptersTenderService.Service.RabbitMqTenderingService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;

namespace IntegrationAdaptersTenderService
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
            services.AddControllers().AddNewtonsoftJson(options =>
                        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );

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

            services.Configure<RabbitMqConfiguration>(GetRabbitConfig);
            services.Configure<ServiceSettings>(GetServiceSettings);
            services.AddSingleton<IRabbitMqConnection, RabbitMqConnection>();
            services.AddSingleton<IRabbitMqTenderingService, RabbitMqTenderingService>();
            services.AddScoped<ITenderRepository, MySqlTenderRepository>();
            services.AddScoped<ITenderService, TenderService>();
            services.AddScoped<ITenderMessageRepository, MySqlTenderMessageRepository>();
            services.AddScoped<ITenderMessageService, TenderMessageService>();
        }
        private void GetRabbitConfig(RabbitMqConfiguration conf)
        {
            conf.Host = Configuration.GetValue<string>("RABBITMQ_HOST") ?? "localhost";
            conf.VHost = Configuration.GetValue<string>("RABBITMQ_VHOST") ?? "/";
            conf.Username = Configuration.GetValue<string>("RABBITMQ_USER") ?? "guest";
            conf.Password = Configuration.GetValue<string>("RABBITMQ_PASSWORD") ?? "guest";
            conf.RetryCount = Configuration.GetValue<int>("RABBITMQ_RETRY");
            conf.RetryWait = Configuration.GetValue<int>("RABBITMQ_RETRY_WAIT");
            conf.ActionBenefitQueueName = Configuration.GetValue<string>("ACTIONBENEFIT_QUEUE") ?? "bolnica-1";
            conf.TenderExchangeName = Configuration.GetValue<string>("TENDER_QUEUE") ?? "tender-bolnica-1";
        }

        private void GetServiceSettings(ServiceSettings conf)
        {
            conf.TenderServiceUrl = Configuration.GetValue<string>("TENDER_SERVICE_URL");
            conf.ActionBenefitServiceUrl = Configuration.GetValue<string>("ACTION_BENEFIT_SERVICE_URL");
            conf.DrugServiceUrl = Configuration.GetValue<string>("DRUG_SERVICE_URL");
            conf.PharmacySystemServiceUrl = Configuration.GetValue<string>("PHARMACY_SYSTEM_SERVICE_URL");
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.ApplicationServices.GetService<IRabbitMqTenderingService>();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
