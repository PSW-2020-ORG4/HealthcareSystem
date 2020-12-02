using Backend.Model.Pharmacies;
﻿using Microsoft.EntityFrameworkCore;
using Model.NotificationSurveyAndFeedback;
using Model.Manager;
using Model.PerformingExamination;
using Model.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Model
{
    public class MyDbContext : DbContext
    {
        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
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
        public DbSet<Pharmacy> Pharmacies { get; set; }
        public DbSet<ActionBenefit> ActionsBenefits { get; set; }
        public DbSet<EquipmentInRooms> EquipmentsInRooms { get; set; }
        public DbSet<Equipment> Equipment { get; set; }
        public DbSet<DrugConsumption> DrugConsumptions { get; set; }

        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Pharmacy>().HasIndex(p => p.ActionsBenefitsExchangeName).IsUnique();
            builder.Entity<EquipmentInRooms>().HasKey(o => new { o.RoomNumber, o.IdEquipment });

        }
    }
}
