using Backend;
using System;
using System.Collections.Generic;
using System.Text;
using Backend.Model;
using Microsoft.EntityFrameworkCore;
using Model.Users;
using Backend.Model.Enums;

namespace UserServiceTests.IntegrationTests
{
    public class DbContextInMemory
    {
        private DbContextOptionsBuilder<MyDbContext> _builder = new DbContextOptionsBuilder<MyDbContext>();
        private DbContextOptions<MyDbContext> _options;
        public MyDbContext _context { get; private set; }
        public DbContextInMemory()
        {
            _builder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            _options = _builder.Options;

        }
        public void SeedDatas()
        {
            _context = new MyDbContext(_options);
            _context.Add(new Country() { Id = 1, Name = "Srbija" });

            _context.Add(new City() { ZipCode = 1, Name = "Beograd", CountryId = 1 });
            _context.Add(new City() { ZipCode = 2, Name = "Novi Sad", CountryId = 1 });
            _context.Add(new City() { ZipCode = 3, Name = "Subotica", CountryId = 1 });

            _context.Add(new Patient()
            {
                Jmbg = "1309998775018",
                Name = "Ana",
                Surname = "Anić",
                DateOfBirth = new DateTime(1998, 9, 13),
                Gender = GenderType.F,
                CityZipCode = 1,
                Email = "ana_anic98@gmail.com",
                HomeAddress = "Tolstojeva 12",
                Password = "11111111",
                DateOfRegistration = DateTime.Now,
                IsActive = true,
                IsBlocked = false,
                Phone = "065897520",
                Username = "ana_anic98@gmail.com",
                ImageName = "picture1.jpg"
            });

            _context.Add(new Admin()
            {
                Jmbg = "0811965521021",
                Name = "Milan",
                Surname = "Milić",
                DateOfBirth = new DateTime(1965, 11, 08),
                Phone = "021954201",
                Email = "milic_milan@gmail.com",
                HomeAddress = "Aleja Svetog Save 100",
                Username = "milic_milan@gmail.com",
                CityZipCode = 3,
                Gender = GenderType.M,
                Password = "milanmilic965"
            });
            _context.SaveChanges();
        }
    }
}
