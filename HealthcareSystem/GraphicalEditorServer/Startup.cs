using Backend.Model;
using Backend.Repository;
using Backend.Repository.DoctorSpecialtyRepository;
using Backend.Repository.DoctorSpecialtyRepository.MySqlDoctorSpecialtyRepository;
using Backend.Repository.DrugInRoomRepository;
using Backend.Repository.DrugInRoomRepository.MySqlDrugInRoomRepository;
using Backend.Repository.DrugRepository;
using Backend.Repository.DrugRepository.MySQLDrugRepository;
using Backend.Repository.DrugTypeRepository;
using Backend.Repository.DrugTypeRepository.MySqlDrugTypeRepository;
using Backend.Repository.EquipmentInExaminationRepository;
using Backend.Repository.EquipmentInExaminationRepository.MySqlEquipmentInExaminationRepository;
using Backend.Repository.EquipmentInRoomsRepository;
using Backend.Repository.EquipmentInRoomsRepository.MySqlEquipmentInRoomsRepository;
using Backend.Repository.EquipmentTransferRepository;
using Backend.Repository.EquipmentTransferRepository.MySqlTransferEquipmentRepository;
using Backend.Repository.ExaminationRepository;
using Backend.Repository.ExaminationRepository.MySqlExaminationRepository;
using Backend.Repository.RenovationPeriodRepository;
using Backend.Repository.RenovationPeriodRepository.MySqlRenovationPeriodRepository;
using Backend.Repository.RoomRepository;
using Backend.Repository.RoomRepository.MySqlRoomRepository;
using Backend.Repository.SpecialtyRepository;
using Backend.Repository.SpecialtyRepository.MySqlSpecialtyRepository;
using Backend.Service;
using Backend.Service.DrugAndTherapy;
using Backend.Service.ExaminationAndPatientCard;
using Backend.Service.RoomAndEquipment;
using Backend.Service.UsersAndWorkingTime;
using Backend.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Repository;
using Service.DrugAndTherapy;
using Service.ExaminationAndPatientCard;
using Service.RoomAndEquipment;
using Service.UsersAndWorkingTime;
using System;
using System.Collections.Generic;

namespace GraphicalEditorServer
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
                int retryCount = Configuration.GetValue<int>("DATABASE_RETRY");
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

            services.AddScoped<IRoomService, RoomService>();
            services.AddScoped<IRoomRepository, MySqlRoomRepository>();
            services.AddScoped<IRenovationPeriodRepository, MySqlRenovationPeriodRepository>();
            services.AddScoped<IEquipmentTypeService, EquipmentTypeService>();

            services.AddScoped<IEquipmentTypeRepository, MySqlEquipmentTypeRepository>();
            services.AddScoped<IEquipmentInExaminationRepository, MySqlEquipmentInExaminationRepository>();

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
            services.AddScoped<IEquipmentInExaminationService, EquipmentInExaminationService>();
            services.AddScoped<IDrugTypeRepository, MySqlDrugTypeRepository>();

            services.AddScoped<ISpecialtyRepository, MySqlSpecialtyRepository>();
            services.AddScoped<ISpecialtyService, SpecialtyService>();

            services.AddScoped<IDoctorRepository, MySqlDoctorRepository>();
            services.AddScoped<IDoctorService, DoctorService>();

            services.AddScoped<IDoctorSpecialtyRepository, MySqlDoctorSpecialtyRepository>();
            services.AddScoped<IDoctorSpecialtyService, DoctorSpecialtyService>();

            services.AddScoped<IActivePatientRepository, MySqlActivePatientRepository>();
            services.AddScoped<IPatientService, PatientService>();

            services.AddScoped<IActivePatientCardRepository, MySqlActivePatientCardRepository>();
            services.AddScoped<IPatientCardService, PatientCardService>();

            services.AddScoped<IExaminationService, ExaminationService>();
            services.AddScoped<IExaminationRepository, MySqlExaminationRepository>();


            services.AddScoped<IFreeAppointmentSearchService, FreeAppointmentSearchService>();
            services.AddScoped<IScheduleAppointmenService, ScheduleAppointmentService>();
            services.AddScoped<IDoctorRepository, MySqlDoctorRepository>();
            services.AddScoped<IActivePatientRepository, MySqlActivePatientRepository>();
            services.AddScoped<IActivePatientCardRepository, MySqlActivePatientCardRepository>();
            services.AddScoped<IRenovationPeriodRepository, MySqlRenovationPeriodRepository>();
 	        services.AddScoped<IEquipmentTransferRepository,MySqlEquipmentTransferRepostory>();
            services.AddScoped<IEquipmentTransferService, EquipmentTransferService>();
            services.AddScoped<IEquipmentInExaminationRepository, MySqlEquipmentInExaminationRepository>();
	        services.AddScoped<IEquipmentInExaminationService, EquipmentInExaminationService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            using (var scope = app.ApplicationServices.CreateScope())
            using (var context = scope.ServiceProvider.GetService<MyDbContext>())
            {
                try
                {
                    Console.WriteLine("Data seeding started.");
                    DataSeeder seeder = new DataSeeder(true);
                    if (seeder.IsAlreadySeeded(context))
                        Console.WriteLine("Data already seeded.");
                    else
                    {
                        context.Database.EnsureDeleted();
                        context.Database.EnsureCreated();
                        seeder.SeedAll(context);
                    }
                    Console.WriteLine("Data seeding finished.");
                }
                catch (Exception e)
                {
                    Console.WriteLine("Data seeding failed.");
                    Console.WriteLine(e.Message);
                    Console.WriteLine(e.StackTrace);
                }
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
