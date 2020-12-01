using Backend.Model;
using Backend.Model.Pharmacies;
using Backend.Repository;
using Backend.Repository.DrugConsumptionRepository;
using Backend.Repository.DrugConsumptionRepository.MySqlDrugConsumptionRepository;
using Backend.Service;
using Backend.Service.DrugConsumptionService;
using Backend.Service.Pharmacies;
using Backend.Service.SftpService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace IntegrationAdapters
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
            services.AddDbContext<MyDbContext>(opt => opt.UseMySql(Configuration.GetConnectionString("PharmacyCooperationConnection")).UseLazyLoadingProxies());
            services.AddControllersWithViews();

            services.Configure<RabbitMqConfiguration>(Configuration.GetSection("RabbitMq"));
            services.Configure<SftpConfig>(Configuration.GetSection("SftpConfig"));
            services.AddSingleton<RabbitMqActionBenefitMessageingService>();
            services.AddSingleton<IHostedService, RabbitMqActionBenefitMessageingService>(ServiceProvider => ServiceProvider.GetService<RabbitMqActionBenefitMessageingService>());

            services.AddScoped<IPharmacyRepo, MySqlPharmacyRepo>();
            services.AddScoped<IPharmacyService, PharmacyService>();
            services.AddScoped<IActionBenefitRepository, MySqlActionBenefitRepository>();
            services.AddScoped<IActionBenefitService, ActionBenefitService>();
            services.AddScoped<ISftpService, SftpService>();
            services.AddScoped<IDrugConsumptionRepository, MySqlDrugConsumptionRepository>();
            services.AddScoped<IDrugConsumptionService, DrugConsumptionService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
