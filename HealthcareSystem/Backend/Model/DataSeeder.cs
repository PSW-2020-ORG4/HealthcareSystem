using Backend.Model.Users;
using Model.Enums;
using Model.Users;
using System;
using System.Collections.Generic;
using System.Text;
using Backend.Model.Enums;
using Backend.Model.Manager;
using Model.Manager;
using Model.PerformingExamination;

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
            SeedSpecialties(context);
            SeedDoctors(context);
            SeedPatientsAndPatientsCard(context);
            SeedRooms(context);
            SeedEquipmentTypes(context);
            SeedEquipments(context);
            SeedEquipmentInRooms(context);
            SeedDrugTypes(context);
            SeedDrugs(context);
            SeedDrugsInRooms(context);
            SeedExaminations(context);
        }

        private void SeedDrugsInRooms(MyDbContext context)
        {
            for (int roomNumber = 1; roomNumber <= 34; roomNumber++)
            {
                context.Add(new DrugInRoom()
                {
                    DrugId = RandomGenerator.Next(1, 4),
                    RoomNumber = roomNumber,
                    Quantity = RandomGenerator.Next(1, 60)
                });
            }
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

        }

        private void SeedDrugTypes(MyDbContext context)
        {
            context.Add(new DrugType() { Type = "tableta", Purpose = "lek za glavu" });
            context.Add(new DrugType() { Type = "tableta", Purpose = "lek za temperaturu" });
            context.Add(new DrugType() { Type = "kapsula", Purpose = "probiotik" });
        }

        private void SeedEquipmentInRooms(MyDbContext context)
        {
            for (int roomNumber = 1; roomNumber <= 34; roomNumber++)
            {
                context.Add(new EquipmentInRooms()
                {
                    RoomNumber = roomNumber,
                    Quantity = RandomGenerator.Next(1, 60)
                });
            }

        }

        private void SeedEquipments(MyDbContext context)
        {
            for (int equipmentIdx = 1; equipmentIdx < 35; equipmentIdx++)
            {
                context.Add(new Equipment()
                {
                    Quantity = RandomGenerator.Next(1, 45),
                    TypeId = RandomGenerator.Next(1, 8)
                });
            }
        }

        private void SeedExaminations(MyDbContext context)
        {
            var evenCounter = 0;
            var oddCounter = 0;
            for (int examinationIdx = 1; examinationIdx < 22; examinationIdx++)
            {
                Examination newExamination = new Examination(){
                    Id = examinationIdx,
                    Type = TypeOfExamination.GENERAL,
                    DoctorJmbg = "1234567891234",
                    IdRoom = 9,
                    IdPatientCard = 2,
                    IsSurveyCompleted = false
                };

                if (examinationIdx % 2 == 0)
                {
                    newExamination.Anamnesis = "Glavobolja";
                    newExamination.DateAndTime = new DateTime(2020, 12, 30, 7 + evenCounter++, 0, 0);
                }
                else
                {
                    newExamination.Anamnesis = "COVID 19";
                    newExamination.DateAndTime = new DateTime(2020, 12, 30, 7 + oddCounter++, 30, 0);
                }

                if (examinationIdx % 3 == 0) newExamination.ExaminationStatus = ExaminationStatus.CANCELED;
                else newExamination.ExaminationStatus = ExaminationStatus.CREATED;
            }

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

            context.Add(new DoctorSpecialty() { DoctorJmbg = "8520147896320", SpecialtyId = 1 });
            context.Add(new DoctorSpecialty() { DoctorJmbg = "1234567891234", SpecialtyId = 2 });

            context.SaveChanges();
        }

        private void SeedRooms(MyDbContext context)
        {

            for (int roomIdx = 1; roomIdx < 8; roomIdx++)
            {
                context.Add(new Room()
                {
                    Usage = TypeOfUsage.CONSULTING_ROOM,
                    Capacity = RandomGenerator.Next(0, 10),
                    Occupation = RandomGenerator.Next(0, 5),
                    Renovation = false
                });
            }

            for (int roomIdx = 1; roomIdx < 8; roomIdx++)
            {
                context.Add(new Room()
                {
                    Usage = TypeOfUsage.SICKROOM,
                    Capacity = RandomGenerator.Next(0, 10),
                    Occupation = RandomGenerator.Next(0, 5),
                    Renovation = false
                });
            }

            for (int roomIdx = 1; roomIdx < 8; roomIdx++)
            {
                context.Add(new Room()
                {
                    Usage = TypeOfUsage.OPERATION_ROOM,
                    Capacity = RandomGenerator.Next(0, 10),
                    Occupation = RandomGenerator.Next(0, 5),
                    Renovation = false
                });
            }

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
        }
    }
}
