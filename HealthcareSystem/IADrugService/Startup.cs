using System;
using System.Collections.Generic;
using Backend.Model;
using Backend.Repository.DrugInRoomRepository;
using Backend.Repository.DrugInRoomRepository.MySqlDrugInRoomRepository;
using Backend.Repository.DrugRepository;
using Backend.Repository.DrugRepository.MySQLDrugRepository;
using Backend.Service.DrugAndTherapy;
using Backend.Settings;
using IntegrationAdaptersDrugService.Repository;
using IntegrationAdaptersDrugService.Repository.Implementation;
using IntegrationAdaptersDrugService.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Service.DrugAndTherapy;

namespace IntegrationAdaptersDrugService
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

            services.AddScoped<IConfirmedDrugRepository, MySqlConfirmedDrugRepository>();
            services.AddScoped<IUnconfirmedDrugRepository, MySqlUnconfirmedDrugRepository>();
            services.AddScoped<IDrugInRoomRepository, MySqlDrugInRoomRepository>();
            services.AddScoped<IDrugService, DrugService>();
            services.AddScoped<IDrugConsumptionRepository, MySqlDrugConsumptionRepository>();
            services.AddScoped<IDrugConsumptionService, DrugConsumptionService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
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
