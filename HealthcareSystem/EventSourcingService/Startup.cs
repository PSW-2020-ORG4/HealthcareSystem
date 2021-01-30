using EventSourcingService.Model;
using EventSourcingService.Model.GraphicalEditor;
using EventSourcingService.Repository;
using EventSourcingService.Service;
using EventSourcingService.Service.GraphicalEditor;
using EventSourcingService.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;

namespace EventSourcingService
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
            if (_env.IsDevelopment())
            {
                Console.WriteLine("Configuring for dev.");
                IConfiguration conf = Configuration.GetSection("DbConnectionSettings");
                DbConnectionSettings dbSettings = conf.Get<DbConnectionSettings>();

                Console.WriteLine(dbSettings.ConnectionString);

                services.AddDbContext<EventSourcingDbContext>(options =>
                {
                    options.UseMySql(dbSettings.ConnectionString,
                        x => x.EnableRetryOnFailure(
                            dbSettings.RetryCount, new TimeSpan(0, 0, 0, dbSettings.RetryWaitInSeconds), new List<int>())
                    ).UseLazyLoadingProxies();
                });
            }
            else if (_env.EnvironmentName.ToLower().Equals("test"))
            {
                Console.WriteLine("Configuring for test.");
                int retryCount = Configuration.GetValue<int>("DATABASE_RETRY");
                int retryWait = Configuration.GetValue<int>("DATABASE_RETRY_WAIT");
                string dbURL = Configuration.GetValue<string>("DATABASE_URL") ?? "postgres://dummy:dummy@dummy:5432/dummy";
                DbConnectionSettings dbSettings = new DbConnectionSettings(dbURL, retryCount, retryWait);

                Console.WriteLine(dbSettings.ConnectionString);

                services.AddDbContext<EventSourcingDbContext>(options =>
                {
                    options.UseNpgsql(
                        dbSettings.ConnectionString + ";SSL Mode=Prefer;Trust Server Certificate=true",
                        x => x.EnableRetryOnFailure(
                            dbSettings.RetryCount, new TimeSpan(0, 0, 0, dbSettings.RetryWaitInSeconds), new List<string>())
                    ).UseLazyLoadingProxies();
                });
            }
            else
            {
                Console.WriteLine("Not dev or test.");
            }


            services.AddControllers().AddNewtonsoftJson(s =>
            {
                s.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            });

            services.AddScoped<IDomainEventRepository<PatientStepSchedulingEvent>, DomainEventRepository<PatientStepSchedulingEvent>>();
            services.AddScoped<IEventStorePatientStepSchedulingService, EventStorePatientStepSchedulingService>();

            services.AddScoped<IDomainEventRepository<PatientStartSchedulingEvent>, DomainEventRepository<PatientStartSchedulingEvent>>();
            services.AddScoped<IEventStorePatientStartSchedulingService, EventStorePatientStartSchedulingService>();

            services.AddScoped<IDomainEventRepository<PatientEndSchedulingEvent>, DomainEventRepository<PatientEndSchedulingEvent>>();
            services.AddScoped<IEventStorePatientEndSchedulingService, EventStorePatientEndSchedulingService>();

            services.AddScoped<IRoomSelectionService, RoomSelectionService>();
            services.AddScoped<IDomainEventRepository<RoomSelectionEvent>, DomainEventRepository<RoomSelectionEvent>>();

            services.AddScoped<IBuildingSelectionService, BuildingSelectionService>();
            services.AddScoped<IDomainEventRepository<BuildingSelectionEvent>, DomainEventRepository<BuildingSelectionEvent>>();

            services.AddScoped<IFloorChangeService, FloorChangeService>();
            services.AddScoped<IDomainEventRepository<FloorChangeEvent>, DomainEventRepository<FloorChangeEvent>>();
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
