using Backend.Model.Pharmacies;
using Microsoft.EntityFrameworkCore;
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
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<PatientCard> PatientCards { get; set; }
        public DbSet<Pharmacy> Pharmacies { get; set; }
        public DbSet<Therapy> Therapies { get; set; }
        public DbSet<Examination> Examinations { get; set; }

        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }

    }
}
