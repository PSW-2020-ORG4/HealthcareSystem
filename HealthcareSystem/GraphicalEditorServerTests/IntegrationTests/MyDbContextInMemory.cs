using Backend;
using Backend.Model;
using Backend.Model.Enums;
using Backend.Model.Manager;
using Backend.Model.PerformingExamination;
using Backend.Model.Users;
using Microsoft.EntityFrameworkCore;
using Model.Manager;
using Model.Users;
using System;

namespace GraphicalEditorServerTests.IntegrationTests
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
            var room1 = new Room { Id = 1, Usage = TypeOfUsage.CONSULTING_ROOM, Capacity = 2, Occupation = 1, Renovation = false };
            var room2 = new Room { Id = 2, Usage = TypeOfUsage.CONSULTING_ROOM, Capacity = 2, Occupation = 1, Renovation = false };
            var patient1 = new Patient
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
            var patient2 = new Patient
            {
                Jmbg = "987654321124",
                Name = "Nina",
                Surname = "Ristic",
                DateOfBirth = DateTime.Now,
                Gender = GenderType.F,
                CityZipCode = 21000,
                HomeAddress = "Zmaj Jovina 10",
                Phone = "43242341",
                Email = "pera@gmail.com",
                Username = "nina",
                Password = "221122",
                DateOfRegistration = DateTime.Now,
                IsBlocked = false,
                IsActive = true
            };
            var patientCard1 = new PatientCard
            {
                Id = 1,
                BloodType = BloodType.A,
                RhFactor = RhFactorType.NEGATIVE,
                Alergies = "",
                MedicalHistory = "",
                HasInsurance = false,
                Lbo = "",
                PatientJmbg = "987654321124"
            };
            var patientCard2 = new PatientCard
            {
                Id = 2,
                BloodType = BloodType.A,
                RhFactor = RhFactorType.NEGATIVE,
                Alergies = "",
                MedicalHistory = "",
                HasInsurance = false,
                Lbo = "",
                PatientJmbg = "1234567891234"
            };
            var speciality = new Specialty { Id = 1, Name = "Kardiolog" };
            var doctor1 = new Doctor
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
            var doctor2 = new Doctor
            {
                Jmbg = "1109965768767",
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
            var doctor1Speciality = new DoctorSpecialty { DoctorJmbg = "0909965768767", SpecialtyId = 1 };
            var doctor2Speciality = new DoctorSpecialty { DoctorJmbg = "1109965768767", SpecialtyId = 1 };
            var examination1 = new Examination
            {
                Id = 1,
                Type = TypeOfExamination.GENERAL,
                DateAndTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(1).Day, 7, 0, 0, DateTimeKind.Utc),
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
                DateAndTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(1).Day, 7, 30, 0, DateTimeKind.Utc),
                Anamnesis = "COVID-19",
                DoctorJmbg = "0909965768767",
                IdRoom = 1,
                IdPatientCard = 1,
                ExaminationStatus = ExaminationStatus.CREATED,
                IsSurveyCompleted = false
            };
            var examination3 = new Examination
            {
                Id = 3,
                Type = TypeOfExamination.GENERAL,
                DateAndTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(1).Day, 8, 0, 0, DateTimeKind.Utc),
                Anamnesis = "COVID-19",
                DoctorJmbg = "0909965768767",
                IdRoom = 1,
                IdPatientCard = 1,
                ExaminationStatus = ExaminationStatus.CREATED,
                IsSurveyCompleted = false
            };
            var examination4 = new Examination
            {
                Id = 4,
                Type = TypeOfExamination.GENERAL,
                DateAndTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(1).Day, 8, 30, 0, DateTimeKind.Utc),
                Anamnesis = "COVID-19",
                DoctorJmbg = "0909965768767",
                IdRoom = 1,
                IdPatientCard = 1,
                ExaminationStatus = ExaminationStatus.CREATED,
                IsSurveyCompleted = false
            };
            var examination5 = new Examination
            {
                Id = 5,
                Type = TypeOfExamination.GENERAL,
                DateAndTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(1).Day, 9, 0, 0, DateTimeKind.Utc),
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
            _context.Add(room1);
            _context.Add(room2);
            _context.Add(patient1);
            _context.Add(patient2);
            _context.Add(patientCard1);
            _context.Add(patientCard2);
            _context.Add(speciality);
            _context.Add(doctor1);
            _context.Add(doctor2);
            _context.Add(doctor1Speciality);
            _context.Add(doctor2Speciality);
            _context.Add(examination1);
            _context.Add(examination2);
            _context.Add(examination3);
            _context.Add(examination4);
            _context.Add(examination5);
            _context.Add(equipmentType);
            _context.Add(equipment);
            _context.Add(equipmentInRoom);
            _context.SaveChanges();
        }

        public void SetEmergencyAppointmentSearchServiceTestIntegration()
        {
            _context = new MyDbContext(_options);
            var country = new Country { Id = 1, Name = "Srbija" };
            var city = new City { ZipCode = 21000, Name = "Novi Sad", CountryId = 1 };
            var room1 = new Room { Id = 1, Usage = TypeOfUsage.CONSULTING_ROOM, Capacity = 2, Occupation = 1, Renovation = false };
            var room2 = new Room { Id = 2, Usage = TypeOfUsage.CONSULTING_ROOM, Capacity = 2, Occupation = 1, Renovation = false };
            var patient1 = new Patient
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
            var patient2 = new Patient
            {
                Jmbg = "987654321124",
                Name = "Nina",
                Surname = "Ristic",
                DateOfBirth = DateTime.Now,
                Gender = GenderType.F,
                CityZipCode = 21000,
                HomeAddress = "Zmaj Jovina 10",
                Phone = "43242341",
                Email = "pera@gmail.com",
                Username = "nina",
                Password = "221122",
                DateOfRegistration = DateTime.Now,
                IsBlocked = false,
                IsActive = true
            };
            var patient3 = new Patient
            {
                Jmbg = "111111111111",
                Name = "Mirko",
                Surname = "Pavlovic",
                DateOfBirth = DateTime.Now,
                Gender = GenderType.F,
                CityZipCode = 21000,
                HomeAddress = "Zmaj Jovina 10",
                Phone = "43242341",
                Email = "pera@gmail.com",
                Username = "nina",
                Password = "221122",
                DateOfRegistration = DateTime.Now,
                IsBlocked = false,
                IsActive = true
            };
            var patientCard1 = new PatientCard
            {
                Id = 1,
                BloodType = BloodType.A,
                RhFactor = RhFactorType.NEGATIVE,
                Alergies = "",
                MedicalHistory = "",
                HasInsurance = false,
                Lbo = "",
                PatientJmbg = "987654321124"
            };
            var patientCard2 = new PatientCard
            {
                Id = 2,
                BloodType = BloodType.A,
                RhFactor = RhFactorType.NEGATIVE,
                Alergies = "",
                MedicalHistory = "",
                HasInsurance = false,
                Lbo = "",
                PatientJmbg = "1234567891234"
            };
            var patientCard3 = new PatientCard
            {
                Id = 3,
                BloodType = BloodType.A,
                RhFactor = RhFactorType.NEGATIVE,
                Alergies = "",
                MedicalHistory = "",
                HasInsurance = false,
                Lbo = "",
                PatientJmbg = "111111111111"
            };
            var speciality = new Specialty { Id = 1, Name = "Kardiolog" };
            var doctor1 = new Doctor
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
            var doctor2 = new Doctor
            {
                Jmbg = "1109965768767",
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
            var doctor1Speciality = new DoctorSpecialty { DoctorJmbg = "0909965768767", SpecialtyId = 1 };
            var doctor2Speciality = new DoctorSpecialty { DoctorJmbg = "1109965768767", SpecialtyId = 1 };
            var examination1 = new Examination
            {
                Id = 1,
                Type = TypeOfExamination.GENERAL,
                DateAndTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(1).Day, 8, 30, 0, DateTimeKind.Utc),
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
                DateAndTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(1).Day, 9, 0, 0, DateTimeKind.Utc),
                Anamnesis = "COVID-19",
                DoctorJmbg = "0909965768767",
                IdRoom = 1,
                IdPatientCard = 1,
                ExaminationStatus = ExaminationStatus.CREATED,
                IsSurveyCompleted = false
            };
            var examination3 = new Examination
            {
                Id = 3,
                Type = TypeOfExamination.GENERAL,
                DateAndTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(1).Day, 9, 30, 0, DateTimeKind.Utc),
                Anamnesis = "COVID-19",
                DoctorJmbg = "0909965768767",
                IdRoom = 1,
                IdPatientCard = 1,
                ExaminationStatus = ExaminationStatus.CREATED,
                IsSurveyCompleted = false
            };
            var examination4 = new Examination
            {
                Id = 4,
                Type = TypeOfExamination.GENERAL,
                DateAndTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(1).Day, 10, 0, 0, DateTimeKind.Utc),
                Anamnesis = "COVID-19",
                DoctorJmbg = "0909965768767",
                IdRoom = 1,
                IdPatientCard = 1,
                ExaminationStatus = ExaminationStatus.CREATED,
                IsSurveyCompleted = false
            };
            var examination5 = new Examination
            {
                Id = 5,
                Type = TypeOfExamination.GENERAL,
                DateAndTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(1).Day, 10, 30, 0, DateTimeKind.Utc),
                Anamnesis = "COVID-19",
                DoctorJmbg = "0909965768767",
                IdRoom = 1,
                IdPatientCard = 1,
                ExaminationStatus = ExaminationStatus.CREATED,
                IsSurveyCompleted = false
            };

            var examination6 = new Examination
            {
                Id = 6,
                Type = TypeOfExamination.GENERAL,
                DateAndTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(1).Day, 8, 30, 0, DateTimeKind.Utc),
                Anamnesis = "Bol u grlu",
                DoctorJmbg = "1109965768767",
                IdRoom = 2,
                IdPatientCard = 2,
                ExaminationStatus = ExaminationStatus.CREATED,
                IsSurveyCompleted = false
            };
            var examination7 = new Examination
            {
                Id = 7,
                Type = TypeOfExamination.GENERAL,
                DateAndTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(1).Day, 9, 0, 0, DateTimeKind.Utc),
                Anamnesis = "COVID-19",
                DoctorJmbg = "1109965768767",
                IdRoom = 2,
                IdPatientCard = 2,
                ExaminationStatus = ExaminationStatus.CREATED,
                IsSurveyCompleted = false
            };
            var examination8 = new Examination
            {
                Id = 8,
                Type = TypeOfExamination.GENERAL,
                DateAndTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(1).Day, 9, 30, 0, DateTimeKind.Utc),
                Anamnesis = "COVID-19",
                DoctorJmbg = "1109965768767",
                IdRoom = 2,
                IdPatientCard = 2,
                ExaminationStatus = ExaminationStatus.CREATED,
                IsSurveyCompleted = false
            };
            var examination9 = new Examination
            {
                Id = 9,
                Type = TypeOfExamination.GENERAL,
                DateAndTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(1).Day, 10, 0, 0, DateTimeKind.Utc),
                Anamnesis = "COVID-19",
                DoctorJmbg = "1109965768767",
                IdRoom = 2,
                IdPatientCard = 2,
                ExaminationStatus = ExaminationStatus.CREATED,
                IsSurveyCompleted = false
            };

            var equipmentType = new EquipmentType { Id = 1, Name = "Krevet", IsConsumable = false };
            var equipment = new Equipment { Id = 1, Quantity = 5, TypeId = 1 };
            var equipmentInRoom = new EquipmentInRooms { IdEquipment = 1, Quantity = 2, RoomNumber = 1 };

            _context.Add(country);
            _context.Add(city);
            _context.Add(room1);
            _context.Add(room2);
            _context.Add(patient1);
            _context.Add(patient2);
            _context.Add(patient3);
            _context.Add(patientCard1);
            _context.Add(patientCard2);
            _context.Add(patientCard3);
            _context.Add(speciality);
            _context.Add(doctor1);
            _context.Add(doctor2);
            _context.Add(doctor1Speciality);
            _context.Add(doctor2Speciality);
            _context.Add(examination1);
            _context.Add(examination2);
            _context.Add(examination3);
            _context.Add(examination4);
            _context.Add(examination5);
            _context.Add(examination6);
            _context.Add(examination7);
            _context.Add(examination8);
            _context.Add(examination9);
            _context.Add(equipmentType);
            _context.Add(equipment);
            _context.Add(equipmentInRoom);
            _context.SaveChanges();
        }
    }
}
