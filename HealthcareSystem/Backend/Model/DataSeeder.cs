using Backend.Model.Users;
using Model.Users;
using System;
using System.Collections.Generic;
using System.Text;
using Backend.Model.Enums;
using Backend.Model.Manager;
using Model.Manager;
using Model.PerformingExamination;
using System.Linq;
using Backend.Model.Pharmacies;
using Model.NotificationSurveyAndFeedback;
using Backend.Model.PerformingExamination;

namespace Backend.Model
{
    public class DataSeeder
    {
        private Random RandomGenerator { get; set; }
        private bool Verbose { get; set; }

        public DataSeeder()
        {
            RandomGenerator = new Random();
            Verbose = false;
        }

        public DataSeeder(bool verbose)
        {
            RandomGenerator = new Random();
            Verbose = verbose;
        }

        public Boolean IsAlreadySeeded(MyDbContext context)
        {
            return context.Countries.Where(c => c.Name.Equals("Srbija")).Count() > 0;
        }

        public void SeedAll(MyDbContext context)
        {
            if (Verbose) Console.WriteLine("Seeding countries.");
            SeedCountries(context);
            if (Verbose) Console.WriteLine("Seeding cities.");
            SeedCities(context);
            if (Verbose) Console.WriteLine("Seeding patients.");
            SeedPatientsAndPatientsCard(context);
            if (Verbose) Console.WriteLine("Seeding specialties.");
            SeedSpecialties(context);
            if (Verbose) Console.WriteLine("Seeding equipment.");
            SeedEquipmentTypes(context);
            if (Verbose) Console.WriteLine("Seeding rooms.");
            SeedRooms(context);
            if (Verbose) Console.WriteLine("Seeding doctors.");
            SeedDoctors(context);
            if (Verbose) Console.WriteLine("Seeding drugs.");
            SeedDrugTypes(context);
            SeedDrugs(context);
            SeedDrugsInRooms(context);            
            if (Verbose) Console.WriteLine("Seeding examinations.");
            SeedExaminations(context);
            if (Verbose) Console.WriteLine("Seeding therapies.");
            SeedTherapies(context);
            if (Verbose) Console.WriteLine("Seeding pharmacies.");
            SeedPharmacies(context);
            if (Verbose) Console.WriteLine("Seeding drug consumptions.");
            SeedDrugConsumptions(context);
            if (Verbose) Console.WriteLine("Seeding feedback.");
            SeedFeedback(context);
            if (Verbose) Console.WriteLine("Seeding admins.");
            SeedAdmins(context);
            if (Verbose) Console.WriteLine("Seeding actions.");
            SeedActionBenefits(context);

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
                DateOfBirth = new DateTime(1998, 9, 13),
                Gender = GenderType.F,
                CityZipCode = 1,
                Email = "ana_anic98@gmail.com",
                HomeAddress = "Tolstojeva 12",
                Password = "11111111",
                DateOfRegistration = DateTime.Now,
                IsActive = true,
                IsBlocked = false,
                IsGuest = false,
                Phone = "065897520",
                Username = "ana_anic98@gmail.com",
                ImageName = "/Uploads/picture1.jpg"
            });
            context.Add(new Patient()
            {
                Jmbg = "2711998896320",
                Name = "Žana",
                Surname = "Žanić",
                DateOfBirth = new DateTime(1998, 11, 27),
                Gender = GenderType.M,
                CityZipCode = 1,
                Email = "zana998@gmail.com",
                HomeAddress = "Bulevar Oslobođenja 100",
                Password = "12345678",
                DateOfRegistration = DateTime.Now,
                IsActive = true,
                IsBlocked = false,
                IsGuest = false,
                Phone = "065897520",
                Username = "zana998@gmail.com",
                ImageName = "/Uploads/profile_pic.jpg"
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
            context.Add(new Specialty() { Name = "Epidemiolog" });
            context.Add(new Specialty() { Name = "Stomatolog" });
            context.Add(new Specialty() { Name = "Neurolog" });
            context.Add(new Specialty() { Name = "Kardiolog" });
            context.Add(new Specialty() { Name = "Opšti" });

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
                DateOfEmployment = new DateTime(2018,05,06),
                CityZipCode = 1,
                Gender = GenderType.F,
                NumberOfLicence = "11111111",
                Password = "MiraMira",
                DoctorsOfficeId = 9
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
                DateOfEmployment = new DateTime(2010,11, 15),
                CityZipCode = 1,
                Gender = GenderType.F,
                NumberOfLicence = "22222222",
                Password = "DaraDara",
                DoctorsOfficeId = 12
            });
            context.Add(new Doctor()
            {
                Jmbg = "0110983520145",
                Name = "Miloš",
                Surname = "Milošević",
                DateOfBirth = new DateTime(1983, 01, 10),
                Phone = "021852145",
                Email = "mmilosevic@gmail.com",
                HomeAddress = "Balzakova 18",
                Username = "mmilosevic@gmail.com",
                DateOfEmployment = new DateTime(2019, 04, 30),
                CityZipCode = 2,
                Gender = GenderType.M,
                NumberOfLicence = "11111111",
                Password = "milosevicm",
                DoctorsOfficeId = 20
            });
            context.Add(new Doctor()
            {
                Jmbg = "0323970501235",
                Name = "Žika",
                Surname = "Žikić",
                DateOfBirth = new DateTime(1970, 03, 23),
                Phone = "021752032",
                Email = "zikazikic@gmail.com",
                HomeAddress = "Narodnog Fronta 7",
                Username = "zikiczile@gmail.com",
                DateOfEmployment = new DateTime(2008, 10, 01),
                CityZipCode = 2,
                Gender = GenderType.M,
                NumberOfLicence = "11111111",
                Password = "zikiczile",
                DoctorsOfficeId = 25
            });
            context.Add(new Doctor()
            {
                Jmbg = "0606988520123",
                Name = "Marija",
                Surname = "Marić",
                DateOfBirth = new DateTime(1988, 06, 06),
                Phone = "021954201",
                Email = "marija988@gmail.com",
                HomeAddress = "Narodnog Fronta 45",
                Username = "marija988@gmail.com",
                DateOfEmployment = new DateTime(2020, 02, 05),
                CityZipCode = 1,
                Gender = GenderType.F,
                NumberOfLicence = "11111111",
                Password = "marija988",
                DoctorsOfficeId = 49
            });
            context.Add(new Doctor()
            {
                Jmbg = "2007975521021",
                Name = "Sara",
                Surname = "Sarić",
                DateOfBirth = new DateTime(1975, 07, 20),
                Phone = "021954201",
                Email = "sarasaric@gmail.com",
                HomeAddress = "Futoška 5",
                Username = "sarasaric@gmail.com",
                DateOfEmployment = new DateTime(2018, 05, 11),
                CityZipCode = 2,
                Gender = GenderType.F,
                NumberOfLicence = "11111111",
                Password = "sarasaric",
                DoctorsOfficeId = 51
            });


            context.SaveChanges();

            context.Add(new DoctorSpecialty() { DoctorJmbg = "8520147896320", SpecialtyId = 1 });
            context.Add(new DoctorSpecialty() { DoctorJmbg = "0110983520145", SpecialtyId = 1 });
            context.Add(new DoctorSpecialty() { DoctorJmbg = "1234567891234", SpecialtyId = 2 });
            context.Add(new DoctorSpecialty() { DoctorJmbg = "0323970501235", SpecialtyId = 2 });
            context.Add(new DoctorSpecialty() { DoctorJmbg = "0606988520123", SpecialtyId = 3 });
            context.Add(new DoctorSpecialty() { DoctorJmbg = "2007975521021", SpecialtyId = 4 });
            context.SaveChanges();
        }

        private void SeedRooms(MyDbContext context)
        {
            List<int> operation = new List<int> { 14, 15, 16, 18, 19, 41, 42, 55, 56, 57, 58, 59, 60 };
            List<int> sick = new List<int> { 32, 33, 34, 35, 36, 37, 38, 39, 40 };

            SeedConsultingRooms(context);

            foreach (int id in operation)
                context.Add(new Room { Id = id, Usage = TypeOfUsage.OPERATION_ROOM });
            foreach (int id in sick)
                context.Add(new Room { Id = id, Usage = TypeOfUsage.SICKROOM });

            context.SaveChanges();
        }

        private void SeedConsultingRooms(MyDbContext context)
        {
            List<int> consulting = new List<int> { 9, 12, 13, 20, 21, 25, 49, 50, 51, 52, 53, 54 };

            List<EquipmentType> equipmentTypes = context.EquipmentTypes.ToList();
            List<List<EquipmentType>> equipmentTypesInRooms = new List<List<EquipmentType>>();
            equipmentTypesInRooms.Add(new List<EquipmentType> { equipmentTypes[0], equipmentTypes[2], equipmentTypes[5], equipmentTypes[7] });
            equipmentTypesInRooms.Add(new List<EquipmentType> { equipmentTypes[1], equipmentTypes[3], equipmentTypes[4] });
            equipmentTypesInRooms.Add(new List<EquipmentType> { equipmentTypes[2], equipmentTypes[6] });
            equipmentTypesInRooms.Add(new List<EquipmentType> { equipmentTypes[1], equipmentTypes[2], equipmentTypes[3] });
            equipmentTypesInRooms.Add(new List<EquipmentType> { equipmentTypes[5], equipmentTypes[6] });
            equipmentTypesInRooms.Add(new List<EquipmentType> { equipmentTypes[1], equipmentTypes[7] });
            equipmentTypesInRooms.Add(new List<EquipmentType> { equipmentTypes[1], equipmentTypes[2], equipmentTypes[6], equipmentTypes[7] });
            equipmentTypesInRooms.Add(new List<EquipmentType> { equipmentTypes[0], equipmentTypes[5], equipmentTypes[6] });
            equipmentTypesInRooms.Add(new List<EquipmentType> { equipmentTypes[4], equipmentTypes[5] });
            equipmentTypesInRooms.Add(new List<EquipmentType> { equipmentTypes[0], equipmentTypes[2], equipmentTypes[7] });
            equipmentTypesInRooms.Add(new List<EquipmentType> { equipmentTypes[5], equipmentTypes[6] });
            equipmentTypesInRooms.Add(new List<EquipmentType> { equipmentTypes[0], equipmentTypes[4] });

            for (int i = 0; i < consulting.Count; i++)
            {
                context.Add(new Room { Id = consulting[i], Usage = TypeOfUsage.CONSULTING_ROOM });
                foreach (EquipmentType equipmentType in equipmentTypesInRooms[i])
                    AddEquipmentToRoom(context, consulting[i], equipmentType);
            }
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

        private void AddEquipmentToRoom(MyDbContext context, int roomId, EquipmentType equipmentType)
        {
            int id = context.Equipment.Count() + 1;
            int quantity = RandomGenerator.Next(1, 10);

            context.Add(new Equipment()
            {
                TypeId = equipmentType.Id,
                Quantity = quantity
            });
            context.SaveChanges();

            context.Add(new EquipmentInRooms()
            {
                IdEquipment = id,
                RoomNumber = roomId,
                Quantity = quantity
            });
            context.SaveChanges();
        }

        private void SeedExaminations(MyDbContext context)
        {
            Doctor doctor = context.Doctors.Find("8520147896320"); 
            PatientCard patientCard = context.PatientCards.Find(2); 
            Room room = context.Rooms.Where(r => r.Usage.Equals(TypeOfUsage.CONSULTING_ROOM)).First();

            DateTime start = DateTime.Now.Date.AddDays(10).AddHours(7);
            DateTime end = DateTime.Now.Date.AddDays(13).AddHours(17);

            for (DateTime current = start; current < end; current = current.AddMinutes(30))
            {
                if (CheckIfTimeValid(current))
                {
                    int examinationId = context.Examinations.Count();
                    context.Add(new Examination
                    {
                        Type = TypeOfExamination.GENERAL,
                        DoctorJmbg = doctor.Jmbg,
                        IdPatientCard = patientCard.Id,
                        IdRoom = room.Id,
                        DateAndTime = current,
                        IsSurveyCompleted = false,
                        ExaminationStatus = ExaminationStatus.CREATED
                    });

                    AddEquipmentInExamination(context, room.Id, examinationId);

                    continue;
                }
                current = new DateTime(current.Year, current.Month, current.Day, 6, 30, 0);
                current = current.AddDays(1);
            }
            context.Add(new Examination
            {
                Type = TypeOfExamination.GENERAL,
                DoctorJmbg = doctor.Jmbg,
                IdPatientCard = patientCard.Id,
                IdRoom = room.Id,
                DateAndTime = DateTime.Now.Date.AddDays(-10).AddHours(8).AddMinutes(30),
                IsSurveyCompleted = false,
                ExaminationStatus = ExaminationStatus.CANCELED
            });
            context.Add(new Examination
            {
                Type = TypeOfExamination.GENERAL,
                DoctorJmbg = doctor.Jmbg,
                IdPatientCard = patientCard.Id,
                IdRoom = room.Id,
                DateAndTime = DateTime.Now.Date.AddDays(-12).AddHours(10),
                IsSurveyCompleted = false,
                ExaminationStatus = ExaminationStatus.CANCELED
            });
            context.Add(new Examination
            {
                Type = TypeOfExamination.GENERAL,
                DoctorJmbg = doctor.Jmbg,
                IdPatientCard = patientCard.Id,
                IdRoom = room.Id,
                DateAndTime = DateTime.Now.Date.AddDays(-15).AddHours(12).AddMinutes(30),
                IsSurveyCompleted = false,
                ExaminationStatus = ExaminationStatus.CANCELED
            });

            patientCard = context.PatientCards.Find(1);
            Doctor doctor1 = context.Doctors.Find("0606988520123");
            Doctor doctor2 = context.Doctors.Find("0323970501235");

            context.Add(new Examination
            {
                Type = TypeOfExamination.GENERAL,
                DoctorJmbg = doctor.Jmbg,
                IdPatientCard = patientCard.Id,
                IdRoom = room.Id,
                DateAndTime = DateTime.Now.Date.AddDays(-15).AddHours(14),
                IsSurveyCompleted = true,
                ExaminationStatus = ExaminationStatus.FINISHED,
                Anamnesis = "Sinuzitis"
            });
            context.Add(new Examination
            {
                Type = TypeOfExamination.GENERAL,
                DoctorJmbg = doctor1.Jmbg,
                IdPatientCard = patientCard.Id,
                IdRoom = room.Id,
                DateAndTime = DateTime.Now.Date.AddDays(-10).AddHours(9).AddMinutes(30),
                IsSurveyCompleted = false,
                ExaminationStatus = ExaminationStatus.FINISHED,
                Anamnesis = "Upala uha"
            });
            context.Add(new Examination
            {
                Type = TypeOfExamination.GENERAL,
                DoctorJmbg = doctor2.Jmbg,
                IdPatientCard = patientCard.Id,
                IdRoom = room.Id,
                DateAndTime = DateTime.Now.Date.AddDays(-5).AddHours(11).AddMinutes(30),
                IsSurveyCompleted = false,
                ExaminationStatus = ExaminationStatus.FINISHED,
                Anamnesis = "Upala pluća"
            });

            context.Add(new Examination
            {
                Type = TypeOfExamination.GENERAL,
                DoctorJmbg = doctor1.Jmbg,
                IdPatientCard = patientCard.Id,
                IdRoom = room.Id,
                DateAndTime = DateTime.Now.Date.AddDays(1).AddHours(10).AddMinutes(30),
                IsSurveyCompleted = false,
                ExaminationStatus = ExaminationStatus.CREATED
            });
            context.Add(new Examination
            {
                Type = TypeOfExamination.GENERAL,
                DoctorJmbg = doctor.Jmbg,
                IdPatientCard = patientCard.Id,
                IdRoom = room.Id,
                DateAndTime = DateTime.Now.Date.AddDays(13).AddHours(7),
                IsSurveyCompleted = false,
                ExaminationStatus = ExaminationStatus.CREATED
            });

            context.Add(new Examination
            {
                Type = TypeOfExamination.GENERAL,
                DoctorJmbg = doctor1.Jmbg,
                IdPatientCard = patientCard.Id,
                IdRoom = room.Id,
                DateAndTime = DateTime.Now.Date.AddDays(-2).AddHours(10),
                IsSurveyCompleted = false,
                ExaminationStatus = ExaminationStatus.CANCELED
            });

            context.SaveChanges();
        }

        private void AddEquipmentInExamination(MyDbContext context, int roomId, int examinationId)
        {
            List<EquipmentInRooms> equipmentInRooms = context.EquipmentsInRooms.Where(e => e.RoomNumber == roomId).ToList();
            List<EquipmentType> equipmentTypes = new List<EquipmentType>();
            foreach (EquipmentInRooms equ in equipmentInRooms)
            {
                Equipment equipment = context.Equipment.Where(e => e.Id == equ.IdEquipment).First();
                equipmentTypes.Add(context.EquipmentTypes.Where(e => e.Id == equipment.TypeId).First());
            }

            List<int> addedEquipment = new List<int>();
            for (int i = 0; i < RandomGenerator.Next(0, equipmentTypes.Count); i++)
            {
                int equipmentTypeId = equipmentTypes[RandomGenerator.Next(0, equipmentTypes.Count)].Id;
                if (!addedEquipment.Contains(equipmentTypeId))
                {
                    context.Add(new EquipmentInExamination { ExaminationId = examinationId, EquipmentTypeID = equipmentTypeId });
                    addedEquipment.Add(equipmentTypeId);
                }
            }
            context.SaveChanges();
        }

        private void SeedTherapies(MyDbContext context)
        {
            List<Examination> previous = context.Examinations.Where(e => e.ExaminationStatus == ExaminationStatus.FINISHED).ToList();
            int maxDrugId = context.Drugs.Count() + 1;

            foreach (Examination e in previous)
            {
                context.Add(new Therapy()
                {
                    IdExamination = e.Id,
                    Diagnosis = e.Anamnesis,
                    StartDate = e.DateAndTime,
                    EndDate = e.DateAndTime.AddDays(RandomGenerator.Next(1, 10)),
                    DailyDose = RandomGenerator.Next(1, 5),
                    IdDrug = RandomGenerator.Next(1, maxDrugId)
                });
            }

            context.SaveChanges();
        }

        private void SeedPharmacies(MyDbContext context)
        {
            context.Add(new PharmacySystem()
            {
                Name = "Janković",
                ApiKey = "ApiKey1",
                Url = "http://localhost:8080",
                ActionsBenefitsExchangeName = "exchange",
                ActionsBenefitsSubscribed = true,
                GrpcHost = "localhost",
                GrpcPort = 30051
            });

            context.SaveChanges();
        }

        private void SeedDrugConsumptions(MyDbContext context)
        {
            DateTime startDate = DateTime.Now;
            DateTime endDate = DateTime.Now.AddDays(10);
            for (DateTime current = startDate; current < endDate; current = current.AddDays(1))
                foreach (Drug drug in context.Drugs)
                    if (RandomGenerator.Next(2) == 1)
                        context.Add(new DrugConsumption()
                        {
                            DrugId = drug.Id,
                            Date = current,
                            Quantity = RandomGenerator.Next(5, 20)
                        });

            context.SaveChanges();
        }

        private void SeedFeedback(MyDbContext context)
        {
            context.Add(new Feedback()
            {
                CommentatorJmbg = "1309998775018",
                Comment = "Sve je super.",
                IsAllowedToPublish = true,
                IsPublished = true,
                SendingDate = new DateTime(2020,10,01)
            });
            context.Add(new Feedback()
            {
                CommentatorJmbg = "1309998775018",
                Comment = "Sviđa mi se Vaša bolnica.",
                IsAllowedToPublish = true,
                IsPublished = false,
                SendingDate = new DateTime(2020, 10, 13)
            });
            context.Add(new Feedback()
            {
                CommentatorJmbg = "2711998896320",
                Comment = "Odlična usluga.",
                IsAllowedToPublish = true,
                IsPublished = true,
                SendingDate = new DateTime(2020, 11, 05)
            });
            context.Add(new Feedback()
            {
                CommentatorJmbg = "2711998896320",
                Comment = "Zadovoljan sam uslugama.",
                IsAllowedToPublish = true,
                IsPublished = false,
                SendingDate = new DateTime(2020, 11, 13)
            });
            context.Add(new Feedback()
            {
                CommentatorJmbg = null,
                Comment = "Najbolji ste, sve pohvale!",
                IsAllowedToPublish = true,
                IsPublished = true,
                SendingDate = new DateTime(2020, 12, 06)
            });
            context.Add(new Feedback()
            {
                CommentatorJmbg = null,
                Comment = "Nisam zadovoljan.",
                IsAllowedToPublish = false,
                IsPublished = false,
                SendingDate = new DateTime(2020, 12, 18)
            });

            context.SaveChanges();
        }
        private bool CheckIfTimeValid(DateTime dateTime)
        {
            if (TimeSpan.Compare(dateTime.TimeOfDay, new TimeSpan(7, 0, 0)) < 0)
                return false;
            if (TimeSpan.Compare(dateTime.TimeOfDay, new TimeSpan(17, 0, 0)) >= 0)
                return false;
            return true;
        }
        private void SeedAdmins(MyDbContext context)
        {
            context.Add(new Admin()
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

            context.SaveChanges();
        }

        private void SeedActionBenefits(MyDbContext context)
        {
            context.Add(new ActionBenefit()
            {
                Id = 1,
                PharmacyId = 1,
                Subject = "Novogodišnji popust",
                Message = "Kapi za oči Proculin Tears na popustu 30%",
                IsPublic = true
            });

            context.Add(new ActionBenefit()
            {
                Id = 2,
                PharmacyId = 1,
                Subject = "Popust na penzionere",
                Message = "Renomal gel za zglobove na popustu 40%",
                IsPublic = true
            });

            context.Add(new ActionBenefit()
            {
                Id = 3,
                PharmacyId = 1,
                Subject = "Novogodišnji popust",
                Message = "Corega pasta za protezu na popustu 50%",
                IsPublic = true
            });

            context.SaveChanges();
        }

    }
}
