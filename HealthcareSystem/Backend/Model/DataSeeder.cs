using Backend.Model.Users;
using Model.Enums;
using Model.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Model
{
    public class DataSeeder
    {
        public void SeedAll(MyDbContext context)
        {
            SeedCountries(context);
            SeedCities(context);
            SeedSpecialties(context);
            SeedDoctors(context);
            SeedPatients(context);
        }

        public void SeedCountries(MyDbContext context)
        {
            context.Add(new Country() { Name = "Srbija" });
            context.Add(new Country() { Name = "Crna Gora" });
            context.Add(new Country() { Name = "BiH" });
            context.Add(new Country() { Name = "Hrvatska" });
            context.SaveChanges();
        }

        public void SeedCities(MyDbContext context)
        {
            context.Add(new City() { Name = "Beograd", CountryId = 1 });
            context.Add(new City() { Name = "Novi Sad", CountryId = 1 });
            context.Add(new City() { Name = "Subotica", CountryId = 1 });
            context.Add(new City() { Name = "Podgorica", CountryId = 2 });
            context.Add(new City() { Name = "Budva", CountryId = 2 });
            context.Add(new City() { Name = "Banja Luka", CountryId = 3 });
            context.Add(new City() { Name = "Sarajevo", CountryId = 3 });
            context.Add(new City() { Name = "Zagreb", CountryId = 4 });
            context.SaveChanges();
        }

        public void SeedSpecialties(MyDbContext context)
        {
            context.Add(new Specialty() { Name = "Opšti" });
            context.Add(new Specialty() { Name = "Kardiolog" });
            context.Add(new Specialty() { Name = "Neurolog" });
            context.Add(new Specialty() { Name = "Stomatolog" });
            context.Add(new Specialty() { Name = "Ginekolog" });
            context.Add(new Specialty() { Name = "Epidemiolog" });
            context.SaveChanges();
        }

        public void SeedPatients(MyDbContext context)
        {
            context.Add(new Patient()
            {
                Jmbg = "1309998775018",
                Name = "Ana",
                Surname = "Anić",
                DateOfBirth = new DateTime(1998, 13, 9),
                Gender = GenderType.F,
                CityZipCode = 1,
                Email = "sladjasavkovic333@gmail.com",
                HomeAddress = "Tolstojeva 12",
                Password = "11111111",
                DateOfRegistration = DateTime.Now,
                IsActive = true,
                IsBlocked = false,
                Phone = "065897520",
                Username = "Ana"
            });
            context.Add(new PatientCard()
            {
                PatientJmbg = "1309998775018",
                BloodType = BloodType.A,
                RhFactor = RhFactorType.POSITIVE,
                HasInsurance = false
            });
            context.Add(new Patient()
            {
                Jmbg = "2711998896320",
                Name = "Pera",
                Surname = "Perić",
                DateOfBirth = new DateTime(1998, 11, 27),
                Gender = GenderType.M,
                CityZipCode = 1,
                Email = "sladjasavkovic333@gmail.com",
                HomeAddress = "Tolstojeva 12",
                Password = "11111111",
                DateOfRegistration = DateTime.Now,
                IsActive = true,
                IsBlocked = false,
                Phone = "065897520",
                Username = "pera",
            });
            context.Add(new PatientCard()
            {
                PatientJmbg = "2711998896320",
                BloodType = BloodType.B,
                RhFactor = RhFactorType.POSITIVE,
                HasInsurance = false
            });
            context.SaveChanges();
        }

        public void SeedDoctors(MyDbContext context)
        {
            context.Add(new Doctor()
            {
                Jmbg = "1234567891234",
                Name = "Mira",
                Surname = "Mirić",
                DateOfBirth = new DateTime(1950, 11, 27),
                Phone = "021457852",
                Email = "mira.miric@gmail.com",
                HomeAddress = "Maršala Tita 102",
                Username = "mira.miric@gmail.com",
                DateOfEmployment = DateTime.Now,
                CityZipCode = 1,
                Gender = GenderType.F,
                NumberOfLicence = "11111111",
                Password = "MiraMira"
            });
            context.Add(new Doctor()
            {
                Jmbg = "8520147896320",
                Name = "Dara",
                Surname = "Darić",
                DateOfBirth = new DateTime(1950, 11, 27),
                Phone = "021457852",
                Email = "dara@gmail.com",
                HomeAddress = "Maršala Tita 102",
                Username = "dara@gmail.com",
                DateOfEmployment = DateTime.Now,
                CityZipCode = 1,
                Gender = GenderType.F,
                NumberOfLicence = "22222222",
                Password = "DaraDara",
            });

            context.Add(new DoctorSpecialty() { DoctorJmbg = "8520147896320", SpecialtyId = 1 });
            context.Add(new DoctorSpecialty() { DoctorJmbg = "1234567891234", SpecialtyId = 2 });

            context.SaveChanges();
        }

        public void SeedEquipmentTypes(MyDbContext context)
        {

        }
    }
}
