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
using Backend.Service.Pharmacies;
using Backend.Service.PlacementInARoomAndRenovationPeriod;
using Backend.Service.RoomAndEquipment;
using Backend.Service.SendingMail;
using Backend.Service.UsersAndWorkingTime;
using Backend.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using PatientWebApp.Settings;
using Repository;
using Service.DrugAndTherapy;
using Service.ExaminationAndPatientCard;
using Service.NotificationSurveyAndFeedback;
using Service.PlacementInARoomAndRenovationPeriod;
using Service.RoomAndEquipment;
using Service.UsersAndWorkingTime;
using System;
using System.Collections.Generic;
using System.Text;

namespace PatientWebApp
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

            services.AddScoped<ISpecialtyRepository, MySqlSpecialtyRepository>();

            services.AddScoped<IActivePatientRepository, MySqlActivePatientRepository>();
            services.AddScoped<IPatientService, PatientService>();

            services.AddScoped<IDoctorRepository, MySqlDoctorRepository>();

            services.AddScoped<IActivePatientCardRepository, MySqlActivePatientCardRepository>();
            services.AddScoped<IPatientCardService, PatientCardService>();

            services.AddScoped<IRoomRepository, MySqlRoomRepository>();
            services.AddScoped<IRenovationPeriodRepository, MySqlRenovationPeriodRepository>();
            services.AddScoped<IRoomService, RoomService>();

            services.AddScoped<IExaminationRepository, MySqlExaminationRepository>();
            services.AddScoped<IExaminationService, ExaminationService>();

            services.AddScoped<IRenovationPeriodRepository, MySqlRenovationPeriodRepository>();
            services.AddScoped<IRenovationPeriodService, RenovationPeriodService>();

            services.AddScoped<IEquipmentRepository, MySqlEquipmentRepository>();
            services.AddScoped<IEquipmentInRoomsRepository, MySqlEquipmentInRoomsRepository>();
            services.AddScoped<IEquipmentService, EquipmentService>();

            services.AddScoped<IFreeAppointmentSearchService, FreeAppointmentSearchService>();

            services.Configure<MailSettings>(Configuration.GetSection("MailSettings"));
            services.Configure<ServiceSettings>(GetServiceSettings);

            services.AddTransient<IMailService, MailService>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            var tokenKey = "This is my private key";
            var key = Encoding.ASCII.GetBytes(tokenKey);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddScoped<IPharmacyRepo, MySqlPharmacyRepo>();
            services.AddScoped<IPharmacyService, PharmacyService>();

            services.AddScoped<IActionBenefitRepository, MySqlActionBenefitRepository>();
            services.AddScoped<IActionBenefitService, ActionBenefitService>();

        }

        private void GetServiceSettings(ServiceSettings conf)
        {
            conf.PatientServiceUrl = Configuration.GetValue<string>("PATIENT_SERVICE_URL");
            conf.FeedbackAndSurveyServiceUrl = Configuration.GetValue<string>("FEEDBACK_SURVEY_SERVICE_URL");
            conf.UserServiceUrl = Configuration.GetValue<string>("USER_SERVICE_URL");
            conf.NotificationServiceUrl = Configuration.GetValue<string>("NOTIFICATION_SERVICE_URL");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            if (env.IsDevelopment() || env.EnvironmentName.ToLower().Equals("test"))
            {
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
                            seeder.SeedAll(context);
                        Console.WriteLine("Data seeding finished.");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Data seeding failed.");
                        Console.WriteLine(e.Message);
                        Console.WriteLine(e.StackTrace);
                    }
                }
            }
            DefaultFilesOptions options = new DefaultFilesOptions();
            options.DefaultFileNames.Clear();
            options.DefaultFileNames.Add("/html/index.html");

            app.UseDefaultFiles(options);

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
