using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Model;
using Backend.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ScheduleService.Model;
using ScheduleService.Repository;
using ScheduleService.Repository.Implementation;
using ScheduleService.Services;

namespace ScheduleService
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
                string dbURL = Configuration.GetValue<string>("DATABASE_URL") ?? "postgres://dummy:dummy@dummy:5432/dummy";
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

            services.AddScoped<IClock, Clock>();

            services.AddScoped<IExaminationService, ExaminationService>();
            services.AddScoped<IAvailableExaminationService, AvailableExaminationService>();

            services.AddScoped<IExaminationRepository, ExaminationRepository>();
            services.AddScoped<IRoomRepository, RoomRepository>();
            services.AddScoped<IDoctorRepository, DoctorRepository>();
            services.AddScoped<IPatientRepository, PatientRepository>();

            services.AddScoped<Backend.Service.RoomAndEquipment.IRoomService, Backend.Service.RoomAndEquipment.RoomService>();
            services.AddScoped<Backend.Repository.RoomRepository.IRoomRepository,
                Backend.Repository.RoomRepository.MySqlRoomRepository.MySqlRoomRepository>();
            services.AddScoped<Backend.Repository.RenovationPeriodRepository.IRenovationPeriodRepository,
                Backend.Repository.RenovationPeriodRepository.MySqlRenovationPeriodRepository.MySqlRenovationPeriodRepository>();
            services.AddScoped<Backend.Repository.EquipmentInRoomsRepository.IEquipmentInRoomsRepository,
                Backend.Repository.EquipmentInRoomsRepository.MySqlEquipmentInRoomsRepository.MySqlEquipmentInRoomsRepository>();
            services.AddScoped<Backend.Repository.IEquipmentRepository, 
                Backend.Repository.MySqlEquipmentRepository>();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseExceptionHandler("/error/dev");
            else
                app.UseExceptionHandler("/error");

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
