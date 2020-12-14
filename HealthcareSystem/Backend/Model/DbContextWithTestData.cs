using Microsoft.EntityFrameworkCore;
using Model.Enums;
using Model.Users;
using System;

namespace Backend.Model
{
    public class DbContextWithTestData : MyDbContext
    {
        private static string connectionString = $"server='dummy' ;userid='dummy'; pwd='dummy';"
                                                + $"port='3306'; database='dummy'";
        private static DbContextOptionsBuilder<MyDbContext> dummy =
            new DbContextOptionsBuilder<MyDbContext>().UseMySql(connectionString);

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Country>().HasData(new Country() { Id = -1, Name = "Srbija" });

            builder.Entity<City>().HasData(new City() { ZipCode = -1, Name = "Novi Sad", CountryId = -1 });

            builder.Entity<Patient>().HasData(new Patient()
            {
                Jmbg = "1114567111234",
                Name = "Pera",
                Surname = "Peric",
                DateOfBirth = DateTime.Now,
                Gender = GenderType.M,
                CityZipCode = -1,
                HomeAddress = "Marka Miljanova 3",
                Phone = "066608927",
                Email = "pera@pera.pera",
                Username = "peraaaaaa",
                Password = "12345678",
                IsActive = false,
                IsBlocked = false
            });
        }

        public DbContextWithTestData(DbContextOptions<MyDbContext> options) : base(options) { }

        public DbContextWithTestData() : base(dummy.Options) { }

    }
}
