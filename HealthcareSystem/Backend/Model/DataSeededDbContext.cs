using Backend.Model.Enums;
using Backend.Model.Manager;
using Backend.Model.PerformingExamination;
using Backend.Model.Pharmacies;
using Backend.Model.Users;
using Microsoft.EntityFrameworkCore;
using Model.Enums;
using Model.Manager;
using Model.NotificationSurveyAndFeedback;
using Model.PerformingExamination;
using Model.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Model
{
    class DataSeededDbContext : MyDbContext
    {
        private Random RandomGenerator { get; set; }

        public DataSeededDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
            RandomGenerator = new Random();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            #region CitiesAndCountries
            builder.Entity<Country>().HasData(
                new Country() { Name = "Srbija", Id = 1 },
                new Country() { Name = "Crna Gora", Id = 2 },
                new Country() { Name = "BiH", Id = 3 },
                new Country() { Name = "Hrvatska", Id = 4 }
                );

            builder.Entity<City>().HasData(
                new City() { Name = "Beograd", CountryId = 1, ZipCode = 1 },
                new City() { Name = "Novi Sad", CountryId = 1, ZipCode = 2 },
                new City() { Name = "Subotica", CountryId = 1, ZipCode = 3 },
                new City() { Name = "Podgorica", CountryId = 2, ZipCode = 4 },
                new City() { Name = "Budva", CountryId = 2, ZipCode = 5 },
                new City() { Name = "Banja Luka", CountryId = 3, ZipCode = 6 },
                new City() { Name = "Sarajevo", CountryId = 3, ZipCode = 7 },
                new City() { Name = "Zagreb", CountryId = 4, ZipCode = 8 }
                );
            #endregion

            #region Patients
            builder.Entity<Patient>().HasData(
                new Patient()
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
                    IsGuest = false,
                    IsActive = true,
                    IsBlocked = false,
                    Phone = "065897520",
                    Username = "ana_anic98@gmail.com",
                    ImageName = "/Uploads/picture1.jpg"
                },
                new Patient()
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
                    IsGuest = false,
                    DateOfRegistration = DateTime.Now,
                    IsActive = true,
                    IsBlocked = false,
                    Phone = "065897520",
                    Username = "zana998@gmail.com",
                    ImageName = "/Uploads/profile_pic.jpg"
                }
                );
            builder.Entity<PatientCard>().HasData(
                new PatientCard()
                {
                    PatientJmbg = "1309998775018",
                    BloodType = BloodType.A,
                    RhFactor = RhFactorType.POSITIVE,
                    HasInsurance = false,
                    Id = -1
                },
                new PatientCard()
                {
                    PatientJmbg = "2711998896320",
                    BloodType = BloodType.B,
                    RhFactor = RhFactorType.POSITIVE,
                    HasInsurance = false,
                    Id = -2
                }
                );
            #endregion

            #region Specialties
            builder.Entity<Specialty>().HasData(
                new Specialty() { Name = "Epidemiolog", Id = 1 },
                new Specialty() { Name = "Stomatolog", Id = 2 },
                new Specialty() { Name = "Neurolog", Id = 3 },
                new Specialty() { Name = "Kardiolog", Id = 4 },
                new Specialty() { Name = "Opšti", Id = 5 }
                );
            #endregion

            #region Doctors
            builder.Entity<Doctor>().HasData(
                new Doctor()
                {
                    Jmbg = "1234567891234",
                    Name = "Mira",
                    Surname = "Mirić",
                    DateOfBirth = new DateTime(1950, 11, 27),
                    Phone = "021457852",
                    Email = "mira.miric@gmail.com",
                    HomeAddress = "Maršala Tita 102",
                    Username = "mira.miric@gmail.com",
                    DateOfEmployment = new DateTime(2018, 05, 06),
                    CityZipCode = 1,
                    Gender = GenderType.F,
                    NumberOfLicence = "11111111",
                    Password = "MiraMira",
                    DoctorsOfficeId = 9
                },
                new Doctor()
                {
                    Jmbg = "8520147896320",
                    Name = "Dara",
                    Surname = "Darić",
                    DateOfBirth = new DateTime(1950, 11, 27),
                    Phone = "021457852",
                    Email = "dara@gmail.com",
                    HomeAddress = "Maršala Tita 102",
                    Username = "dara@gmail.com",
                    DateOfEmployment = new DateTime(2010, 11, 15),
                    CityZipCode = 1,
                    Gender = GenderType.F,
                    NumberOfLicence = "22222222",
                    Password = "DaraDara",
                    DoctorsOfficeId = 12
                },
                new Doctor()
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
                },
                new Doctor()
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
                },
                new Doctor()
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
                },
                new Doctor()
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

            builder.Entity<DoctorSpecialty>().HasData(
                new DoctorSpecialty() { DoctorJmbg = "8520147896320", SpecialtyId = 1 },
                new DoctorSpecialty() { DoctorJmbg = "0110983520145", SpecialtyId = 1 },
                new DoctorSpecialty() { DoctorJmbg = "1234567891234", SpecialtyId = 2 },
                new DoctorSpecialty() { DoctorJmbg = "0323970501235", SpecialtyId = 2 },
                new DoctorSpecialty() { DoctorJmbg = "0606988520123", SpecialtyId = 3 },
                new DoctorSpecialty() { DoctorJmbg = "2007975521021", SpecialtyId = 4 }
                );
            #endregion

            #region Rooms
            List<int> consulting = new List<int> { 9, 12, 13, 20, 21, 25, 49, 50, 51, 52, 53, 54 };
            List<int> operation = new List<int> { 14, 15, 16, 18, 19, 41, 42, 55, 56, 57, 58, 59, 60 };
            List<int> sick = new List<int> { 32, 33, 34, 35, 36, 37, 38, 39, 40 };

            foreach (int id in consulting)
                builder.Entity<Room>().HasData(new Room { Id = id, Usage = TypeOfUsage.CONSULTING_ROOM });
            foreach (int id in operation)
                builder.Entity<Room>().HasData(new Room { Id = id, Usage = TypeOfUsage.OPERATION_ROOM });
            foreach (int id in sick)
                builder.Entity<Room>().HasData(new Room { Id = id, Usage = TypeOfUsage.SICKROOM });
            #endregion

            #region DrugTypes
            builder.Entity<DrugType>().HasData(
                new DrugType() { Type = "tableta", Purpose = "lek za glavu", Id = 1 },
                new DrugType() { Type = "tableta", Purpose = "lek za temperaturu", Id = 2 },
                new DrugType() { Type = "kapsula", Purpose = "probiotik", Id = 3 }
                );
            #endregion

            #region EquipmentTypes
            builder.Entity<EquipmentType>().HasData(
                new EquipmentType() { Name = "needle", IsConsumable = true, Id = 1 },
                new EquipmentType() { Name = "bend", IsConsumable = true, Id = 2 },
                new EquipmentType() { Name = "mask", IsConsumable = true, Id = 3 },
                new EquipmentType() { Name = "bed", IsConsumable = false, Id = 4 },
                new EquipmentType() { Name = "table", IsConsumable = false, Id = 5 },
                new EquipmentType() { Name = "computer", IsConsumable = false, Id = 6 },
                new EquipmentType() { Name = "chair", IsConsumable = false, Id = 7 },
                new EquipmentType() { Name = "instrument", IsConsumable = false, Id = 8 }
                );
            #endregion

            #region DrugTypes
            builder.Entity<Drug>().HasData(
                new Drug()
                {
                    DrugType_Id = 1,
                    Name = "Brufen",
                    Quantity = RandomGenerator.Next(5, 30),
                    ExpirationDate = new DateTime(2021, 12, 13, 1, 8, 57),
                    Producer = "Hemofarm",
                    Id = 1
                },
                new Drug()
                {
                    DrugType_Id = 2,
                    Name = "Metafeks",
                    Quantity = RandomGenerator.Next(5, 30),
                    ExpirationDate = new DateTime(2021, 12, 13, 1, 8, 57),
                    Producer = "Hemofarm",
                    Id = 2
                },
                new Drug()
                {
                    DrugType_Id = 1,
                    Name = "Aspirin",
                    Quantity = RandomGenerator.Next(5, 30),
                    ExpirationDate = new DateTime(2021, 12, 13, 1, 8, 57),
                    Producer = "Galenika",
                    Id = 3
                },
                new Drug()
                {
                    DrugType_Id = 3,
                    Name = "Bulardi",
                    Quantity = RandomGenerator.Next(5, 30),
                    ExpirationDate = new DateTime(2021, 12, 13, 1, 8, 57),
                    Producer = "Hemofarm",
                    Id = 4
                });
            #endregion

            #region DrugsInRooms
            for (int i = 1; i <= 4; i++)
                foreach (int roomId in consulting)
                    if (RandomGenerator.Next(2) == 1)
                        builder.Entity<DrugInRoom>().HasData(new DrugInRoom()
                        {
                            DrugId = i,
                            RoomNumber = roomId,
                            Quantity = RandomGenerator.Next(10, 20)
                        });
            for (int i = 1; i <= 4; i++)
                foreach (int roomId in operation)
                    if (RandomGenerator.Next(2) == 1)
                        builder.Entity<DrugInRoom>().HasData(new DrugInRoom()
                        {
                            DrugId = i,
                            RoomNumber = roomId,
                            Quantity = RandomGenerator.Next(10, 20)
                        });
            for (int i = 1; i <= 4; i++)
                foreach (int roomId in sick)
                    if (RandomGenerator.Next(2) == 1)
                        builder.Entity<DrugInRoom>().HasData(new DrugInRoom()
                        {
                            DrugId = i,
                            RoomNumber = roomId,
                            Quantity = RandomGenerator.Next(10, 20)
                        });
            #endregion

            #region EquipmentInRooms
            int eid = -1;
            for (int typeId = 1; typeId <= 8; typeId++)
                foreach (int roomId in operation)
                    if (RandomGenerator.Next(10) > 7)
                    {
                        int quantity = RandomGenerator.Next(10, 20);

                        builder.Entity<Equipment>().HasData(new Equipment()
                        {
                            TypeId = typeId,
                            Quantity = quantity,
                            Id = eid
                        });

                        builder.Entity<EquipmentInRooms>().HasData(new EquipmentInRooms()
                        {
                            IdEquipment = eid,
                            RoomNumber = roomId,
                            Quantity = quantity
                        });

                        eid--;
                    }
            for (int typeId = 1; typeId <= 8; typeId++)
                foreach (int roomId in consulting)
                    if (RandomGenerator.Next(10) > 7)
                    {
                        int quantity = RandomGenerator.Next(10, 20);

                        builder.Entity<Equipment>().HasData(new Equipment()
                        {
                            TypeId = typeId,
                            Quantity = quantity,
                            Id = eid
                        });

                        builder.Entity<EquipmentInRooms>().HasData(new EquipmentInRooms()
                        {
                            IdEquipment = eid,
                            RoomNumber = roomId,
                            Quantity = quantity
                        });

                        eid--;
                    }
            for (int typeId = 1; typeId <= 8; typeId++)
                foreach (int roomId in sick)
                    if (RandomGenerator.Next(10) > 7)
                    {
                        int quantity = RandomGenerator.Next(10, 20);

                        builder.Entity<Equipment>().HasData(new Equipment()
                        {
                            TypeId = typeId,
                            Quantity = quantity,
                            Id = eid
                        });

                        builder.Entity<EquipmentInRooms>().HasData(new EquipmentInRooms()
                        {
                            IdEquipment = eid,
                            RoomNumber = roomId,
                            Quantity = quantity
                        });

                        eid--;
                    }
            #endregion

            #region Feedback
            builder.Entity<Feedback>().HasData(
                new Feedback()
                {
                    Id = -1,
                    CommentatorJmbg = "1309998775018",
                    Comment = "Sve je super.",
                    IsAllowedToPublish = true,
                    IsPublished = true,
                    SendingDate = new DateTime(2020, 10, 01)
                },
                new Feedback()
                {
                    Id = -2,
                    CommentatorJmbg = "1309998775018",
                    Comment = "Sviđa mi se Vaša bolnica.",
                    IsAllowedToPublish = true,
                    IsPublished = false,
                    SendingDate = new DateTime(2020, 10, 13)
                },
                new Feedback()
                {
                    Id = -3,
                    CommentatorJmbg = "2711998896320",
                    Comment = "Odlična usluga.",
                    IsAllowedToPublish = true,
                    IsPublished = true,
                    SendingDate = new DateTime(2020, 11, 05)
                },
                new Feedback()
                {
                    Id = -4,
                    CommentatorJmbg = "2711998896320",
                    Comment = "Zadovoljan sam uslugama.",
                    IsAllowedToPublish = true,
                    IsPublished = false,
                    SendingDate = new DateTime(2020, 11, 13)
                },
                new Feedback()
                {
                    Id = -5,
                    CommentatorJmbg = null,
                    Comment = "Najbolji ste, sve pohvale!",
                    IsAllowedToPublish = true,
                    IsPublished = true,
                    SendingDate = new DateTime(2020, 12, 06)
                },
                new Feedback()
                {
                    Id = -6,
                    CommentatorJmbg = null,
                    Comment = "Nisam zadovoljan.",
                    IsAllowedToPublish = false,
                    IsPublished = false,
                    SendingDate = new DateTime(2020, 12, 18)
                }
                );
            #endregion

            #region Admin
            builder.Entity<Admin>().HasData(
                new Admin
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
                }
                );
            #endregion

            #region Pharmacies
            builder.Entity<PharmacySystem>(
                b =>
                {
                    b.HasData(new PharmacySystem()
                    {
                        Id = -1,
                        Name = "Janković",
                        ApiKey = "ApiKey1",
                        Url = "http://localhost:8080",
                        ActionsBenefitsExchangeName = "seeded",
                        ActionsBenefitsSubscribed = true
                    });
                    b.OwnsOne(e => e.GrpcAdress).HasData(new
                    {
                        PharmacySystemId = -1,
                        GrpcHost = "localhost",
                        GrpcPort = 30051
                    });
                });
            #endregion

            #region ActionBenefits
            builder.Entity<ActionBenefit>(b =>
            {
                b.HasData(new ActionBenefit()
                {
                    Id = -1,
                    PharmacyId = -1,
                    IsPublic = true
                },
                new ActionBenefit()
                {
                    Id = -2,
                    PharmacyId = -1,
                    IsPublic = true
                },
                new ActionBenefit()
                {
                    Id = -3,
                    PharmacyId = -1,
                    IsPublic = true
                });
                b.OwnsOne(e => e.Message).HasData(new
                {
                    ActionBenefitId = -1,
                    Subject = "Novogodišnji popust",
                    Message = "Kapi za oči Proculin Tears na popustu 30%"
                },
                new
                {
                    ActionBenefitId = -2,
                    Subject = "Popust za penzionere",
                    Message = "Renomal gel za zglobove na popustu 40%"
                },
                new
                {
                    ActionBenefitId = -3,
                    Subject = "Novogodišnji popust",
                    Message = "Corega pasta za protezu na popustu 50%"
                });
            });
            #endregion

            #region DrugConsumptions
            int consumptionId = -1;
            DateTime startDate = DateTime.Now;
            DateTime endDate = DateTime.Now.AddDays(10);
            for (DateTime current = startDate; current < endDate; current = current.AddDays(1))
                for (int drugId = 1; drugId <= 4; drugId++)
                    if (RandomGenerator.Next(2) == 1)
                        builder.Entity<DrugConsumption>().HasData(new DrugConsumption()
                        {
                            Id = consumptionId--,
                            DrugId = drugId,
                            Date = current,
                            Quantity = RandomGenerator.Next(5, 20)
                        });
            #endregion

            #region Examinations
            DateTime start = DateTime.Now.Date.AddDays(10).AddHours(7);
            DateTime end = DateTime.Now.Date.AddDays(13).AddHours(17);

            int examid = -1;

            for (DateTime current = start; current < end; current = current.AddMinutes(30))
            {
                if (CheckIfTimeValid(current))
                {
                    builder.Entity<Examination>().HasData(new Examination
                    {
                        Id = examid--,
                        Type = TypeOfExamination.GENERAL,
                        DoctorJmbg = "8520147896320",
                        IdPatientCard = -2,
                        IdRoom = consulting[0],
                        DateAndTime = current,
                        IsSurveyCompleted = false,
                        ExaminationStatus = ExaminationStatus.CREATED
                    });
                    continue;
                }
                current = new DateTime(current.Year, current.Month, current.Day, 6, 30, 0);
                current = current.AddDays(1);
            }
            builder.Entity<Examination>().HasData(
                new Examination
                {
                    Id = examid--,
                    Type = TypeOfExamination.GENERAL,
                    DoctorJmbg = "8520147896320",
                    IdPatientCard = -2,
                    IdRoom = 9,
                    DateAndTime = DateTime.Now.Date.AddDays(-10).AddHours(8).AddMinutes(30),
                    IsSurveyCompleted = false,
                    ExaminationStatus = ExaminationStatus.CANCELED
                },
                new Examination
                {
                    Id = examid--,
                    Type = TypeOfExamination.GENERAL,
                    DoctorJmbg = "8520147896320",
                    IdPatientCard = -2,
                    IdRoom = 9,
                    DateAndTime = DateTime.Now.Date.AddDays(-12).AddHours(10),
                    IsSurveyCompleted = false,
                    ExaminationStatus = ExaminationStatus.CANCELED
                },
                new Examination
                {
                    Id = examid--,
                    Type = TypeOfExamination.GENERAL,
                    DoctorJmbg = "8520147896320",
                    IdPatientCard = -2,
                    IdRoom = 9,
                    DateAndTime = DateTime.Now.Date.AddDays(-15).AddHours(12).AddMinutes(30),
                    IsSurveyCompleted = false,
                    ExaminationStatus = ExaminationStatus.CANCELED
                }
                );

            List<Examination> finished = new List<Examination>() { new Examination
                {
                    Id = examid--,
                    Type = TypeOfExamination.GENERAL,
                    DoctorJmbg = "8520147896320",
                    IdPatientCard = -1,
                    IdRoom = 9,
                    DateAndTime = DateTime.Now.Date.AddDays(-15).AddHours(14),
                    IsSurveyCompleted = true,
                    ExaminationStatus = ExaminationStatus.FINISHED,
                    Anamnesis = "Sinuzitis"
                },
                new Examination
                {
                    Id = examid--,
                    Type = TypeOfExamination.GENERAL,
                    DoctorJmbg = "0606988520123",
                    IdPatientCard = -1,
                    IdRoom = 9,
                    DateAndTime = DateTime.Now.Date.AddDays(-10).AddHours(9).AddMinutes(30),
                    IsSurveyCompleted = false,
                    ExaminationStatus = ExaminationStatus.FINISHED,
                    Anamnesis = "Upala uha"
                }
            };
            builder.Entity<Examination>().HasData(finished);
            builder.Entity<Examination>().HasData(
                new Examination
                {
                    Id = examid--,
                    Type = TypeOfExamination.GENERAL,
                    DoctorJmbg = "0323970501235",
                    IdPatientCard = -1,
                    IdRoom = 9,
                    DateAndTime = DateTime.Now.Date.AddDays(-5).AddHours(11).AddMinutes(30),
                    IsSurveyCompleted = false,
                    ExaminationStatus = ExaminationStatus.FINISHED,
                    Anamnesis = "Upala pluća"
                },
                new Examination
                {
                    Id = examid--,
                    Type = TypeOfExamination.GENERAL,
                    DoctorJmbg = "0606988520123",
                    IdPatientCard = -1,
                    IdRoom = 9,
                    DateAndTime = DateTime.Now.Date.AddDays(1).AddHours(10).AddMinutes(30),
                    IsSurveyCompleted = false,
                    ExaminationStatus = ExaminationStatus.CREATED
                },
                new Examination
                {
                    Id = examid--,
                    Type = TypeOfExamination.GENERAL,
                    DoctorJmbg = "8520147896320",
                    IdPatientCard = -1,
                    IdRoom = 9,
                    DateAndTime = DateTime.Now.Date.AddDays(13).AddHours(7),
                    IsSurveyCompleted = false,
                    ExaminationStatus = ExaminationStatus.CREATED
                },
                new Examination
                {
                    Id = examid--,
                    Type = TypeOfExamination.GENERAL,
                    DoctorJmbg = "0606988520123",
                    IdPatientCard = -1,
                    IdRoom = 9,
                    DateAndTime = DateTime.Now.Date.AddDays(-2).AddHours(10),
                    IsSurveyCompleted = false,
                    ExaminationStatus = ExaminationStatus.CANCELED
                }
                );
            #endregion

            #region Therapies
            int therapyId = -1;
            foreach (Examination e in finished)
            {
                builder.Entity<Therapy>().HasData(
                    new Therapy()
                    {
                        Id = therapyId--,
                        IdExamination = e.Id,
                        Diagnosis = e.Anamnesis,
                        StartDate = e.DateAndTime,
                        EndDate = e.DateAndTime.AddDays(RandomGenerator.Next(1, 10)),
                        DailyDose = RandomGenerator.Next(1, 5),
                        IdDrug = RandomGenerator.Next(1, 4)
                    }
                    );
            }
            #endregion
        }

        private bool CheckIfTimeValid(DateTime dateTime)
        {
            if (TimeSpan.Compare(dateTime.TimeOfDay, new TimeSpan(7, 0, 0)) < 0)
                return false;
            if (TimeSpan.Compare(dateTime.TimeOfDay, new TimeSpan(17, 0, 0)) >= 0)
                return false;
            return true;
        }
    }
}
