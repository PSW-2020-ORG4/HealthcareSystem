using Backend.Model;
using Backend.Repository.RoomRepository;
using Backend.Repository.RoomRepository.MySqlRoomRepository;
using Backend.Service.RoomAndEquipment;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Service.RoomAndEquipment;
using Microsoft.EntityFrameworkCore;
using Backend.Repository.EquipmentInRoomsRepository.MySqlEquipmentInRoomsRepository;
using Backend.Repository.EquipmentInRoomsRepository;
using Backend.Repository;
using Backend.Repository.RenovationPeriodRepository;
using Repository;
using System.Collections.Generic;
using System;

namespace GraphicalEditorServer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            var host = Configuration["DBHOST"] ?? "localhost";
            var port = Configuration["DBPORT"] ?? "3306";
            var user = Configuration["DBUSER"] ?? "organization4";
            var password = Configuration["DBPASSWORD"] ?? "organization4";
            var database = Configuration["DB"] ?? "organization4db";

            string connectionString = $"server={host} ;userid={user}; pwd={password};"
                                    + $"port={port}; database={database}";

            services.AddDbContext<MyDbContext>(options =>
            {
                options.UseMySql(
                    connectionString,
                    x => x.MigrationsAssembly("Backend").EnableRetryOnFailure(
                        100, new TimeSpan(0, 0, 0, 30), new List<int>())
                    ).UseLazyLoadingProxies();
            });

            services.AddScoped<IRoomService, RoomService>();

            services.AddScoped<IRoomRepository, MySqlRoomRepository>();

            services.AddScoped<IEquipmentInRoomsService, EquipmentInRoomsService>();

            services.AddScoped<IRenovationPeriodRepository, RenovationPeriodRepository>();

            services.AddScoped<IEquipmentInRoomsRepository, MySqlEquipmentInRoomsRepository>();

            services.AddScoped<IConsumableEquipmentService, ConsumableEquipmentService>();

            services.AddScoped<IConsumableEquipmentRepository, MySqlConsumableEquipmentRepository>();

            services.AddScoped<INonConsumableEquipmentService, NonConsumableEquipmentService>();

            services.AddScoped<INonConsumableEquipmentRepository, MySqlNonConsumableEquipmentRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, MyDbContext context)
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
