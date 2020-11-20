using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Model;
using Backend.Repository;
using Backend.Repository.DrugRepository;
using Backend.Repository.DrugRepository.MySQLDrugRepository;
using Backend.Repository.DrugTypeRepository;
using Backend.Repository.DrugTypeRepository.MySqlDrugTypeRepository;
using Backend.Repository.ExaminationRepository;
using Backend.Repository.ExaminationRepository.MySqlExaminationRepository;
using Backend.Repository.IngridientRepository;
using Backend.Repository.IngridientRepository.MySqlIngridientRepository;
using Backend.Repository.RenovationPeriodRepository;
using Backend.Repository.RenovationPeriodRepository.MySqlRenovationPeriodRepository;
using Backend.Repository.RoomRepository;
using Backend.Repository.RoomRepository.MySqlRoomRepository;
using Backend.Service.DrugAndTherapy;
using Backend.Service.NotificationSurveyAndFeedback;
using Backend.Service;
using Backend.Service.PlacementInARoomAndRenovationPeriod;
using Backend.Service.RoomAndEquipment;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Repository;
using Service.DrugAndTherapy;
using Service.NotificationSurveyAndFeedback;
using Service.PlacementInARoomAndRenovationPeriod;
using Service.RoomAndEquipment;
using Service.UsersAndWorkingTime;
using Service.ExaminationAndPatientCard;

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
            services.AddDbContext<MyDbContext>(options =>
                                options.UseMySql(ConfigurationExtensions.GetConnectionString(Configuration, "MyDbContextConnectionString")).UseLazyLoadingProxies());

            services.AddScoped<IFeedbackRepository, MySqlFeedbackRepository>();       
            services.AddScoped<IFeedbackService, FeedbackService>();

            services.AddScoped<IActivePatientRepository, MySqlActivePatientRepository>();
            services.AddScoped<IPatientService, PatientService>();

            services.AddScoped<IActivePatientCardRepository, MySqlActivePatientCardRepository>();
            services.AddScoped<IPatientCardService, PatientCardService>();

            services.AddScoped<IConfirmedDrugRepository, MySqlConfirmedDrugRepository>();
            services.AddScoped<IUnconfirmedDrugRepository, MySqlUnconfirmedDrugRepository>();
            services.AddScoped<IDrugService, DrugService>();

            services.AddScoped<IDrugTypeRepository, MySqlDrugTypeRepository>();
            services.AddScoped<IIngridientRepository, MySqlIngridientRepository>();
            services.AddScoped<IDrugTypeAndIngridientService, DrugTypeAndIngridientService>();

            services.AddScoped<IRoomRepository, MySqlRoomRepository>();
            services.AddScoped<IRenovationPeriodRepository, MySqlRenovationPeriodRepository>();
            services.AddScoped<IRoomService, RoomService>();

            //placement dodati
            services.AddScoped<IScheduledExaminationRepository, MySqlScheduledExaminationRepository>();
            services.AddScoped<IRenovationPeriodRepository, MySqlRenovationPeriodRepository>();
            services.AddScoped<IRenovationPeriodService, RenovationPeriodService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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
