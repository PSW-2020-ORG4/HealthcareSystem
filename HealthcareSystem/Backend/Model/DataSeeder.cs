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
using System.Linq;
using Backend.Model.Pharmacies;
using Model.NotificationSurveyAndFeedback;

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
            if (Verbose) Console.WriteLine("Seeding rooms.");
            SeedRooms(context);
            if (Verbose) Console.WriteLine("Seeding doctors.");
            SeedDoctors(context);
            if (Verbose) Console.WriteLine("Seeding drugs.");
            SeedDrugTypes(context);
            SeedDrugs(context);
            SeedDrugsInRooms(context);
            if (Verbose) Console.WriteLine("Seeding equipment.");
            SeedEquipmentTypes(context);
            SeedEquipmentInRooms(context);
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
                Phone = "065897520",
                Username = "ana_anic98@gmail.com",
                ImageName = "picture1.jpg"
            });
            context.Add(new Patient()
            {
                Jmbg = "2711998896320",
                Name = "Pera",
                Surname = "Perić",
                DateOfBirth = new DateTime(1998, 11, 27),
                Gender = GenderType.M,
                CityZipCode = 1,
                Email = "peraperic@gmail.com",
                HomeAddress = "Bulevar Oslobođenja 100",
                Password = "12345678",
                DateOfRegistration = DateTime.Now,
                IsActive = true,
                IsBlocked = false,
                Phone = "065897520",
                Username = "peraperic@gmail.com",
                ImageName = "profile_pic.jpg"
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
            Doctor doctor = context.Doctors.Find("8520147896320"); //Ovo je doktor Dara
            PatientCard patientCard = context.PatientCards.Find("2711998896320"); //Ovo je pacijent Pera
            Room room = context.Rooms.Where(r => r.Usage.Equals(TypeOfUsage.CONSULTING_ROOM)).First();

            DateTime start_future = DateTime.Now.Date.AddDays(5);
            DateTime end_future = DateTime.Now.Date.AddDays(7);
            DateTime start = start_future.AddHours(7);
            DateTime end = end_future.AddHours(16).AddMinutes(30);

            //Pacijent Pera zakazuje sve termine za datume 25. i 27. decembar
            for (DateTime current = start; current < end; current = current.AddMinutes(30))
            {
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
            }
            //Pacijent Pera je otkazao 3 pregleda u zadnjih mjesec dana, pa je maliciozni
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
            //Izabrani parametri: specijalnost-Opsta, doktor-Dara Daric, interval-25. do 27.
            //Prioritet je doktor -> Svi termini su zauzeti pa pretraga izbacuje termine izvan zadatog intervala
            //Prioritet je interval -> Svi termini kod Dare su zauzeti, pa pretraga izbacuje termine kod doktora Milosa

            patientCard = context.PatientCards.Find("1309998775018"); //Ovo je pacijent Ana
            Doctor doctor1 = context.Doctors.Find("0606988520123"); //Ovo je doktor Marija
            Doctor doctor2 = context.Doctors.Find("0323970501235"); //Ovo je doktor Zika

            //Dodajem zavrsene preglede za Anu
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

            //Dodajem zakazane preglede za Anu
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

            //Dodajem otkazane preglede za Anu
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

        private void SeedTherapies(MyDbContext context)
        {
            List<Examination> previous = context.Examinations.Where(e => e.ExaminationStatus == ExaminationStatus.FINISHED).ToList();
            int maxDrugId = context.Drugs.Count() + 1;

            foreach (Examination e in previous)
            {
                if (RandomGenerator.Next(10) > 3)
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
                Name = "Jankovic",
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
                IsPublished = true
            });
            context.Add(new Feedback()
            {
                CommentatorJmbg = "1309998775018",
                Comment = "Sviđa mi se Vaša bolnica.",
                IsAllowedToPublish = true,
                IsPublished = false
            });
            context.Add(new Feedback()
            {
                CommentatorJmbg = "2711998896320",
                Comment = "Odlična usluga.",
                IsAllowedToPublish = true,
                IsPublished = true
            });
            context.Add(new Feedback()
            {
                CommentatorJmbg = "2711998896320",
                Comment = "Zadovoljan sam uslugama.",
                IsAllowedToPublish = true,
                IsPublished = false
            });
            context.Add(new Feedback()
            {
                CommentatorJmbg = null,
                Comment = "Najbolji ste, sve pohvale!",
                IsAllowedToPublish = true,
                IsPublished = true
            });
            context.Add(new Feedback()
            {
                CommentatorJmbg = null,
                Comment = "Nisam zadovoljan.",
                IsAllowedToPublish = false,
                IsPublished = false
            });

            context.SaveChanges();
        }

    }
}
