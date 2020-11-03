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
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }

        //this method will be removed when I finish all refactoring about database
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>().HasData(
                new City { ZipCode = 21000, Name = "Novi Sad" }
            );

            modelBuilder.Entity<Patient>().HasData(
                new Patient
                {
                    Jmbg = "2305992104895",
                    Name = "Marko",
                    Surname = "Marković",
                    DateOfBirth = new DateTime(1992, 5, 23),
                    Email = "",
                    Gender = GenderType.M,
                    HomeAddress = "",
                    CityZipCode = 21000,
                    IsGuest = true,
                    DateOfRegistration = DateTime.Now,
                    Password = "123",
                    Username = "marko",
                    Phone = ""
                }
            );

            modelBuilder.Entity<Admin>().HasData(
               new Admin
               {
                   Jmbg = "0606960851265",
                   Name = "Pera",
                   Surname = "Perić",
                   DateOfBirth = new DateTime(1960, 6, 6),
                   Email = "",
                   Gender = GenderType.M,
                   HomeAddress = "",
                   CityZipCode = 21000,
                   Password = "123",
                   Username = "pera",
                   Phone = ""
               }
           );

            modelBuilder.Entity<Feedback>().HasData(
               new Feedback
               {
                   Id = 1,
                   Comment = "Zadovoljan sam uslugama bolnice.",
                   IsPublished = true,
                   CommentatorJmbg = "2305992104895",
               }
            ); ;

        }
    }
}
