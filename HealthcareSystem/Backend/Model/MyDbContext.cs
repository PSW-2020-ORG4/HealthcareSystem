using Backend.Model.Pharmacies;
using Microsoft.EntityFrameworkCore;
using Model.NotificationSurveyAndFeedback;
using Model.Manager;
using Model.PerformingExamination;
using Model.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.Model.Manager;
using Backend.Model.Users;
using Backend.Model.PerformingExamination;

namespace Backend.Model
{
    public class MyDbContext : DbContext
    {
        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Specialty> Specialties { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<PatientCard> PatientCards { get; set; }
        public DbSet<SurveyAboutDoctor> SurveysAboutDoctor { get; set; }
        public DbSet<SurveyAboutMedicalStaff> SurveysAboutMedicalStaff { get; set; }
        public DbSet<SurveyAboutHospital> SurveysAboutHospital { get; set; }
        public DbSet<Ingredient> Ingridients { get; set; }
        public DbSet<Drug> Drugs { get; set; }
        public DbSet<DrugType> DrugTypes { get; set; }
        public DbSet<Therapy> Therapies { get; set; }
        public DbSet<Examination> Examinations { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RenovationPeriod> RenovationPeriods { get; set; }
        public DbSet<WorkingTime> WorkingTimes { get; set; }
        public DbSet<PharmacySystem> Pharmacies { get; set; }
        public DbSet<ActionBenefit> ActionsBenefits { get; set; }
        public DbSet<EquipmentInRooms> EquipmentsInRooms { get; set; }
        public DbSet<Equipment> Equipment { get; set; }
        public DbSet<EquipmentType> EquipmentTypes { get; set; }
        public DbSet<DoctorSpecialty> DoctorSpecialties { get; set; }
        public DbSet<DrugConsumption> DrugConsumptions { get; set; }
	    public DbSet<DrugInRoom> DrugsInRooms { get; set; }
        public DbSet<EquipmentTransfer> EqupmentTransfer { get; set; }
	    public DbSet<EquipmentInExamination> EquipmentInExamination { get; set; }

        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<PharmacySystem>().HasIndex(p => p.ActionsBenefitsExchangeName).IsUnique();
            builder.Entity<EquipmentInRooms>().HasKey(o => new { o.RoomNumber, o.IdEquipment });
	        builder.Entity<EquipmentInExamination>().HasKey(o => new { o.EquipmentTypeID, o.ExaminationId });
            builder.Entity<DoctorSpecialty>().HasKey(ds => new { ds.DoctorJmbg, ds.SpecialtyId });
            builder.Entity<DoctorSpecialty>().HasOne(ds => ds.Doctor).WithMany(d => d.DoctorSpecialties).HasForeignKey(ds => ds.DoctorJmbg);
            builder.Entity<DoctorSpecialty>().HasOne(ds => ds.Specialty).WithMany(s => s.DoctorSpecialties).HasForeignKey(ds => ds.SpecialtyId);

            builder.Entity<DrugInRoom>().HasKey(o => new { o.RoomNumber, o.DrugId });

        }
    }
}
