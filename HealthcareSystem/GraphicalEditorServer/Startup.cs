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
using Backend.Repository.RenovationPeriodRepository;
using Backend.Repository.RenovationPeriodRepository.MySqlRenovationPeriodRepository;
using Backend.Repository.RoomRepository;
using Backend.Repository.RoomRepository.MySqlRoomRepository;
using Backend.Repository.SpecialtyRepository;
using Backend.Repository.SpecialtyRepository.MySqlSpecialtyRepository;
using Backend.Service;
using Backend.Service.DrugAndTherapy;
using Backend.Service.RoomAndEquipment;
using Backend.Service.UsersAndWorkingTime;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Repository;
using Service.DrugAndTherapy;
using Service.RoomAndEquipment;
using Service.UsersAndWorkingTime;

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
