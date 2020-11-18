using Microsoft.EntityFrameworkCore;
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
        public DbSet<City> Cities { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<PatientCard> PatientCards { get; set; }
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }

    }
}
