using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Model;
using Backend.Repository.RoomRepository;
using Backend.Repository.RoomRepository.MySqlRoomRepository;
using Backend.Service.RoomAndEquipment;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Service.RoomAndEquipment;
using Backend.Model;
using Backend.Repository.RoomRepository;
using Backend.Repository.RoomRepository.MySqlRoomRepository;
using Backend.Service.RoomAndEquipment;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Service.RoomAndEquipment;
using Backend.Repository.EquipmentInRoomsRepository.MySqlEquipmentInRoomsRepository;
using Backend.Repository.EquipmentInRoomsRepository;
using Backend.Repository;
using Backend.Repository.RenovationPeriodRepository;
using Repository;
using Backend.Repository.RenovationPeriodRepository.MySqlRenovationPeriodRepository;

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
            services.AddDbContext<MyDbContext>(options =>
                               options.UseMySql(ConfigurationExtensions.GetConnectionString(Configuration, "MyDbContextConnectionString")).UseLazyLoadingProxies());
            services.AddScoped<IRoomService, RoomService>();

            services.AddScoped<IRoomRepository, MySqlRoomRepository>();

            services.AddScoped<IEquipmentInRoomsService, EquipmentInRoomsService>();

            services.AddScoped<IRenovationPeriodRepository, MySqlRenovationPeriodRepository>();

            services.AddScoped<IEquipmentInRoomsRepository, MySqlEquipmentInRoomsRepository>();

            services.AddScoped<IConsumableEquipmentService, ConsumableEquipmentService>();

            services.AddScoped<IConsumableEquipmentRepository, MySqlConsumableEquipmentRepository>();

            services.AddScoped<INonConsumableEquipmentService, NonConsumableEquipmentService>();

            services.AddScoped<INonConsumableEquipmentRepository, MySqlNonConsumableEquipmentRepository>();
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
