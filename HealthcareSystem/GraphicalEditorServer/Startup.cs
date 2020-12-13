using Backend.Model;
using Backend.Repository;
using Backend.Repository.DrugInRoomRepository;
using Backend.Repository.DrugInRoomRepository.MySqlDrugInRoomRepository;
using Backend.Repository.DrugRepository;
using Backend.Repository.DrugRepository.MySQLDrugRepository;
using Backend.Repository.DrugTypeRepository;
using Backend.Repository.DrugTypeRepository.MySqlDrugTypeRepository;
using Backend.Repository.EquipmentInRoomsRepository;
using Backend.Repository.EquipmentInRoomsRepository.MySqlEquipmentInRoomsRepository;
using Backend.Repository.ExaminationRepository;
using Backend.Repository.ExaminationRepository.MySqlExaminationRepository;
using Backend.Repository.RenovationPeriodRepository;
using Backend.Repository.RenovationPeriodRepository.MySqlRenovationPeriodRepository;
using Backend.Repository.RoomRepository;
using Backend.Repository.RoomRepository.MySqlRoomRepository;
using Backend.Service.DrugAndTherapy;
using Backend.Service.ExaminationAndPatientCard;
using Backend.Service.RoomAndEquipment;
using Backend.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Repository;
using Service.DrugAndTherapy;
using Service.RoomAndEquipment;
using System;
using System.Collections.Generic;

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

            services.AddScoped<IRoomService, RoomService>();
            services.AddScoped<IRoomRepository, MySqlRoomRepository>();
            services.AddScoped<IRenovationPeriodRepository, MySqlRenovationPeriodRepository>();
            services.AddScoped<IEquipmentTypeService, EquipmentTypeService>();

            services.AddScoped<IEquipmentTypeRepository, MySqlEquipmentTypeRepository>();

            services.AddScoped<IEquipmentInRoomsService, EquipmentInRoomsService>();

            services.AddScoped<IEquipmentInRoomsRepository, MySqlEquipmentInRoomsRepository>();

            services.AddScoped<IEquipmentService, EquipmentService>();

            services.AddScoped<IEquipmentRepository, MySqlEquipmentRepository>();

            services.AddScoped<IDrugInRoomService, DrugInRoomService>();

            services.AddScoped<IDrugInRoomRepository, MySqlDrugInRoomRepository>();

            services.AddScoped<IDrugService, DrugService>();

            services.AddScoped<IConfirmedDrugRepository, MySqlConfirmedDrugRepository>();

            services.AddScoped<IUnconfirmedDrugRepository, MySqlUnconfirmedDrugRepository>();

            services.AddScoped<IDrugTypeService, DrugTypeService>();

            services.AddScoped<IDrugTypeRepository, MySqlDrugTypeRepository>();

            services.AddScoped<IFreeAppointmentSearchService, FreeAppointmentSearchService>();

            services.AddScoped<IExaminationRepository, MySqlExaminationRepository>();
            
            services.AddScoped<IExaminationRepository, MySqlExaminationRepository>();

            services.AddScoped<IDoctorRepository, MySqlDoctorRepository>();

            services.AddScoped<IActivePatientCardRepository, MySqlActivePatientCardRepository>();

            services.AddScoped<IRenovationPeriodRepository, MySqlRenovationPeriodRepository>();
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
