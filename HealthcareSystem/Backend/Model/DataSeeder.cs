﻿using Backend.Model.Users;
using Model.Enums;
using Model.Users;
using System;
using System.Collections.Generic;
using System.Text;
using Backend.Model.Enums;
using Backend.Model.Manager;
using Model.Manager;
using Model.PerformingExamination;
using System.Linq;

namespace Backend.Model
{
    public class DataSeeder
    {
        public Random RandomGenerator { get; set; }
        public void SeedAll(MyDbContext context)
        {
            RandomGenerator = new Random();
            SeedCountries(context);
            SeedCities(context);
            SeedPatientsAndPatientsCard(context);
            SeedSpecialties(context);
            SeedDoctors(context);
            SeedRooms(context);
            SeedDrugTypes(context);
            SeedEquipmentTypes(context);
            SeedDrugs(context);
            SeedDrugsInRooms(context);
            SeedEquipmentInRooms(context);
            SeedExaminations(context);

            context.SaveChanges();
        }

        private void SeedCountries(MyDbContext context)
        {
            context.Add(new Country() { Name = "Srbija" });
            context.Add(new Country() { Name = "Crna Gora" });
            context.Add(new Country() { Name = "BiH" });
            context.Add(new Country() { Name = "Hrvatska" });
            context.SaveChanges();
        }

        private void SeedCities(MyDbContext context)
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

        private void SeedPatientsAndPatientsCard(MyDbContext context)
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
            context.SaveChanges();

            context.Add(new PatientCard()
            {
                PatientJmbg = "2711998896320",
                BloodType = BloodType.B,
                RhFactor = RhFactorType.POSITIVE,
                HasInsurance = false
            });
            context.Add(new PatientCard()
            {
                PatientJmbg = "1309998775018",
                BloodType = BloodType.A,
                RhFactor = RhFactorType.POSITIVE,
                HasInsurance = false
            });
            context.SaveChanges();
        }

        private void SeedSpecialties(MyDbContext context)
        {
            context.Add(new Specialty() { Name = "Opšti" });
            context.Add(new Specialty() { Name = "Kardiolog" });
            context.Add(new Specialty() { Name = "Neurolog" });
            context.Add(new Specialty() { Name = "Stomatolog" });
            context.Add(new Specialty() { Name = "Ginekolog" });
            context.Add(new Specialty() { Name = "Epidemiolog" });
            context.SaveChanges();
        }

        private void SeedDoctors(MyDbContext context)
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
            context.SaveChanges();

            context.Add(new DoctorSpecialty() { DoctorJmbg = "8520147896320", SpecialtyId = 1 });
            context.Add(new DoctorSpecialty() { DoctorJmbg = "1234567891234", SpecialtyId = 2 });
            context.SaveChanges();
        }

        private void SeedRooms(MyDbContext context)
        {
            List<int> consulting = new List<int> { 9, 12, 13, 20, 21, 25, 49, 50, 51, 52, 53, 54 };
            List<int> operation = new List<int> { 14, 15, 16, 18, 19, 41, 42, 55, 56, 57, 58, 59, 60 };
            List<int> sick = new List<int> { 32, 33, 34, 35, 36, 37, 38, 39, 40 };

            foreach (int id in consulting)
                context.Add(new Room { Id = id, Usage = TypeOfUsage.CONSULTING_ROOM });
            foreach (int id in operation)
                context.Add(new Room { Id = id, Usage = TypeOfUsage.OPERATION_ROOM });
            foreach (int id in sick)
                context.Add(new Room { Id = id, Usage = TypeOfUsage.SICKROOM });

            context.SaveChanges();
        }

        private void SeedDrugTypes(MyDbContext context)
        {
            context.Add(new DrugType() { Type = "tableta", Purpose = "lek za glavu" });
            context.Add(new DrugType() { Type = "tableta", Purpose = "lek za temperaturu" });
            context.Add(new DrugType() { Type = "kapsula", Purpose = "probiotik" });

            context.SaveChanges();
        }

        private void SeedEquipmentTypes(MyDbContext context)
        {
            context.Add(new EquipmentType() { Name = "needle", IsConsumable = true });
            context.Add(new EquipmentType() { Name = "bend", IsConsumable = true });
            context.Add(new EquipmentType() { Name = "mask", IsConsumable = true });
            context.Add(new EquipmentType() { Name = "bed", IsConsumable = false });
            context.Add(new EquipmentType() { Name = "table", IsConsumable = false });
            context.Add(new EquipmentType() { Name = "computer", IsConsumable = false });
            context.Add(new EquipmentType() { Name = "chair", IsConsumable = false });
            context.Add(new EquipmentType() { Name = "instrument", IsConsumable = false });

            context.SaveChanges();
        }

        private void SeedDrugs(MyDbContext context)
        {
            context.Add(new Drug()
            {
                DrugType_Id = 1,
                Name = "Brufen",
                Quantity = RandomGenerator.Next(5, 30),
                ExpirationDate = new DateTime(2021, 12, 13, 1, 8, 57),
                Producer = "Hemofarm"
            });
            context.Add(new Drug()
            {
                DrugType_Id = 2,
                Name = "Metafeks",
                Quantity = RandomGenerator.Next(5, 30),
                ExpirationDate = new DateTime(2021, 12, 13, 1, 8, 57),
                Producer = "Hemofarm"
            });
            context.Add(new Drug()
            {
                DrugType_Id = 1,
                Name = "Aspirin",
                Quantity = RandomGenerator.Next(5, 30),
                ExpirationDate = new DateTime(2021, 12, 13, 1, 8, 57),
                Producer = "Galenika"
            });
            context.Add(new Drug()
            {
                DrugType_Id = 3,
                Name = "Bulardi",
                Quantity = RandomGenerator.Next(5, 30),
                ExpirationDate = new DateTime(2021, 12, 13, 1, 8, 57),
                Producer = "Hemofarm"
            });

            context.SaveChanges();
        }

        private void SeedDrugsInRooms(MyDbContext context)
        {
            foreach (Room room in context.Rooms)
                foreach (Drug drug in context.Drugs)
                    if (RandomGenerator.Next(2) == 1)
                        context.Add(new DrugInRoom()
                        {
                            DrugId = drug.Id,
                            RoomNumber = room.Id,
                            Quantity = RandomGenerator.Next(10, 20)
                        });

            context.SaveChanges();
        }
        
        private void SeedEquipmentInRooms(MyDbContext context)
        {
            foreach (Room room in context.Rooms)
                foreach (EquipmentType equipmentType in context.EquipmentTypes)
                    if (RandomGenerator.Next(10) > 7)
                        AddEquipmentToRoom(context, room, equipmentType);
        }

        private void AddEquipmentToRoom(MyDbContext context, Room room, EquipmentType equipmentType)
        {
            int id = context.Equipment.Count() + 1;
            int quantity = RandomGenerator.Next(10, 20);

            context.Add(new Equipment()
            {
                TypeId = equipmentType.Id,
                Quantity = quantity
            });
            context.SaveChanges();

            context.Add(new EquipmentInRooms()
            {
                IdEquipment = id,
                RoomNumber = room.Id,
                Quantity = quantity
            });
            context.SaveChanges();
        }
                     
        private void SeedExaminations(MyDbContext context)
        {
            Doctor doctor = context.Doctors.First();
            PatientCard patientCard = context.PatientCards.First();
            Room room = context.Rooms.Where(r => r.Usage.Equals(TypeOfUsage.CONSULTING_ROOM)).First();
            DateTime start = new DateTime(2020, 12, 30, 7, 0, 0);
            DateTime end = new DateTime(2020, 12, 30, 16, 30, 0);

            for(DateTime current = start; current < end; current = current.AddMinutes(30))
            {
                context.Add(new Examination
                {
                    Type = TypeOfExamination.GENERAL,
                    DoctorJmbg = doctor.Jmbg,
                    IdPatientCard = patientCard.Id,
                    IdRoom = room.Id,
                    DateAndTime = current,
                    IsSurveyCompleted = false,
                    ExaminationStatus = (RandomGenerator.Next(10) > 7) ? ExaminationStatus.CREATED : ExaminationStatus.CANCELED
                });
            }

            context.SaveChanges();
        }

    }
}
