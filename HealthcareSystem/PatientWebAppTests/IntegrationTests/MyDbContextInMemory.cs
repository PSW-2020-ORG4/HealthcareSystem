﻿using Backend;
using Backend.Model;
using Backend.Model.Enums;
using Backend.Model.Manager;
using Backend.Model.PerformingExamination;
using Backend.Model.Users;
using Microsoft.EntityFrameworkCore;
using Model.Manager;
using Model.Users;
using System;

namespace PatientWebAppTests.IntegrationTests
{
    public class MyDbContextInMemory
    {
        private DbContextOptionsBuilder<MyDbContext> _builder = new DbContextOptionsBuilder<MyDbContext>();
        private DbContextOptions<MyDbContext> _options;
        public MyDbContext _context { get; private set; }

        public MyDbContextInMemory()
        {
            _builder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            _options = _builder.Options;

        }
        public void SetFreeAppointmentSearchServiceTestIntegration()
        {
            _context = new MyDbContext(_options);
            var country = new Country { Id = 1, Name = "Srbija" };
            var city = new City { ZipCode = 21000, Name = "Novi Sad", CountryId = 1 };
            var room = new Room { Id = 1, Usage = TypeOfUsage.CONSULTING_ROOM, Capacity = 2, Occupation = 1, Renovation = false };
            var patient = new Patient
            {
                Jmbg = "1234567891234",
                Name = "Pera",
                Surname = "Peric",
                DateOfBirth = DateTime.Now,
                Gender = GenderType.M,
                CityZipCode = 21000,
                HomeAddress = "Zmaj Jovina 10",
                Phone = "065452102",
                Email = "pera@gmail.com",
                Username = "pera",
                Password = "12345678",
                DateOfRegistration = DateTime.Now,
                IsBlocked = false,
                IsActive = true
            };
            var patientCard = new PatientCard
            {
                Id = 1,
                BloodType = BloodType.A,
                RhFactor = RhFactorType.NEGATIVE,
                Alergies = "",
                MedicalHistory = "",
                HasInsurance = false,
                Lbo = "",
                PatientJmbg = "1234567891234"
            };
            var speciality = new Specialty { Id = 1, Name = "Kardiolog" };
            var doctor = new Doctor
            {
                Jmbg = "0909965768767",
                Name = "Mira",
                Surname = "Markovic",
                DateOfBirth = DateTime.Now,
                Gender = GenderType.F,
                CityZipCode = 21000,
                HomeAddress = "Zmaj Jovina 10",
                Phone = "065452102",
                Email = "mira@gmail.com",
                Username = "mira",
                Password = "12345678",
                NumberOfLicence = "",
                DoctorsOfficeId = 1,
                DateOfEmployment = DateTime.Now
            };
            var doctorSpeciality = new DoctorSpecialty { DoctorJmbg = "0909965768767", SpecialtyId = 1 };
            var examination1 = new Examination
            {
                Id = 1,
                Type = TypeOfExamination.GENERAL,
                DateAndTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(1).Day, 7, 0, 0),
                Anamnesis = "Bol u grlu",
                DoctorJmbg = "0909965768767",
                IdRoom = 1,
                IdPatientCard = 1,
                ExaminationStatus = ExaminationStatus.CREATED,
                IsSurveyCompleted = false
            };
            var examination2 = new Examination
            {
                Id = 2,
                Type = TypeOfExamination.GENERAL,
                DateAndTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(1).Day, 7, 30, 0),
                Anamnesis = "COVID-19",
                DoctorJmbg = "0909965768767",
                IdRoom = 1,
                IdPatientCard = 1,
                ExaminationStatus = ExaminationStatus.CREATED,
                IsSurveyCompleted = false
            };
            var equipmentType = new EquipmentType { Id = 1, Name = "Krevet", IsConsumable = false };
            var equipment = new Equipment { Id = 1, Quantity = 5, TypeId = 1 };
            var equipmentInRoom = new EquipmentInRooms { IdEquipment = 1, Quantity = 2, RoomNumber = 1 };

            _context.Add(country);
            _context.Add(city);
            _context.Add(room);
            _context.Add(patient);
            _context.Add(patientCard);
            _context.Add(speciality);
            _context.Add(doctor);
            _context.Add(doctorSpeciality);
            _context.Add(examination1);
            _context.Add(examination2);
            _context.Add(equipmentType);
            _context.Add(equipment);
            _context.Add(equipmentInRoom);
            _context.SaveChanges();
        }
    }
}
