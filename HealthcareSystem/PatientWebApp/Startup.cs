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
using Backend.Repository.EquipmentInRoomsRepository;
using Backend.Repository.EquipmentInRoomsRepository.MySqlEquipmentInRoomsRepository;
using Backend.Repository.ExaminationRepository;
using Backend.Repository.ExaminationRepository.MySqlExaminationRepository;
using Backend.Repository.IngridientRepository;
using Backend.Repository.IngridientRepository.MySqlIngridientRepository;
using Backend.Repository.RenovationPeriodRepository;
using Backend.Repository.RenovationPeriodRepository.MySqlRenovationPeriodRepository;
using Backend.Repository.RoomRepository;
using Backend.Repository.RoomRepository.MySqlRoomRepository;
using Backend.Repository.SpecialtyRepository;
using Backend.Repository.SpecialtyRepository.MySqlSpecialtyRepository;
using Backend.Repository.TherapyRepository;
using Backend.Repository.TherapyRepository.MySqlTherapyRepository;
using Backend.Service;
using Backend.Service.DrugAndTherapy;
using Backend.Service.ExaminationAndPatientCard;
using Backend.Service.NotificationSurveyAndFeedback;
using Backend.Service.PlacementInARoomAndRenovationPeriod;
using Backend.Service.RoomAndEquipment;
using Backend.Service.SendingMail;
using Backend.Service.UsersAndWorkingTime;
using Backend.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Repository;
using Service.DrugAndTherapy;
using Service.ExaminationAndPatientCard;
using Service.NotificationSurveyAndFeedback;
using Service.PlacementInARoomAndRenovationPeriod;
using Service.RoomAndEquipment;
using Service.UsersAndWorkingTime;
using System;
using System.Collections.Generic;

namespace PatientWebApp
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

            services.AddScoped<ICountryRepository, MySqlCountryRepository>();
            services.AddScoped<ICountryService, CountryService>();

            services.AddScoped<ICityRepository, MySqlCityRepository>();
            services.AddScoped<ICityService, CityService>();

            services.AddScoped<ISpecialtyRepository, MySqlSpecialtyRepository>();
            services.AddScoped<ISpecialtyService, SpecialtyService>();

            services.AddScoped<IFeedbackRepository, MySqlFeedbackRepository>();
            services.AddScoped<IFeedbackService, FeedbackService>();

            services.AddScoped<IActivePatientRepository, MySqlActivePatientRepository>();
            services.AddScoped<IPatientService, PatientService>();

            services.AddScoped<IDoctorRepository, MySqlDoctorRepository>();
            services.AddScoped<IDoctorService, DoctorService>();

            services.AddScoped<IDoctorSpecialtyRepository, MySqlDoctorSpecialtyRepository>();
            services.AddScoped<IDoctorSpecialtyService, DoctorSpecialtyService>();

            services.AddScoped<ISurveyRepository, MySqlSurveyRepository>();
            services.AddScoped<ISurveyService, SurveyService>();

            services.AddScoped<IActivePatientCardRepository, MySqlActivePatientCardRepository>();
            services.AddScoped<IPatientCardService, PatientCardService>();

            services.AddScoped<IConfirmedDrugRepository, MySqlConfirmedDrugRepository>();
            services.AddScoped<IUnconfirmedDrugRepository, MySqlUnconfirmedDrugRepository>();
            services.AddScoped<IDrugInRoomRepository, MySqlDrugInRoomRepository>();
            services.AddScoped<IDrugService, DrugService>();

            services.AddScoped<IDrugTypeRepository, MySqlDrugTypeRepository>();
            services.AddScoped<IIngridientRepository, MySqlIngridientRepository>();
            services.AddScoped<IDrugTypeAndIngridientService, DrugTypeAndIngridientService>();

            services.AddScoped<IRoomRepository, MySqlRoomRepository>();
            services.AddScoped<IRenovationPeriodRepository, MySqlRenovationPeriodRepository>();
            services.AddScoped<IRoomService, RoomService>();

            services.AddScoped<IExaminationRepository, MySqlExaminationRepository>();
            services.AddScoped<IExaminationService, ExaminationService>();

            services.AddScoped<ITherapyRepository, MySqlTherapyRepository>();
            services.AddScoped<ITherapyService, TherapyService>();

            services.AddScoped<IRenovationPeriodRepository, MySqlRenovationPeriodRepository>();
            services.AddScoped<IRenovationPeriodService, RenovationPeriodService>();

            services.AddScoped<IEquipmentRepository, MySqlEquipmentRepository>();
            services.AddScoped<IEquipmentInRoomsRepository, MySqlEquipmentInRoomsRepository>();
            services.AddScoped<IEquipmentService, EquipmentService>();

            services.AddScoped<IFreeAppointmentSearchService, FreeAppointmentSearchService>();

            services.Configure<MailSettings>(Configuration.GetSection("MailSettings"));

            services.AddTransient<IMailService, MailService>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, MyDbContext context)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
