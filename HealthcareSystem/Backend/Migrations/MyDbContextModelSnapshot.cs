﻿// <auto-generated />
using System;
using Backend.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Backend.Migrations
{
    [DbContext(typeof(MyDbContext))]
    partial class MyDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Backend.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("Backend.Model.ActionBenefit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("IsPublic")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("PharmacyId")
                        .HasColumnType("int");

                    b.Property<string>("Subject")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("PharmacyId");

                    b.ToTable("ActionsBenefits");
                });

            modelBuilder.Entity("Backend.Model.Pharmacies.Pharmacy", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ActionsBenefitsExchangeName")
                        .IsRequired()
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4")
                        .HasMaxLength(255);

                    b.Property<bool>("ActionsBenefitsSubscribed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("ApiKey")
                        .IsRequired()
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4")
                        .HasMaxLength(255);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4")
                        .HasMaxLength(255);

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4")
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.HasIndex("ActionsBenefitsExchangeName")
                        .IsUnique();

                    b.ToTable("Pharmacies");
                });

            modelBuilder.Entity("Backend.Model.Survey", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("AvailabilityOfDoctor")
                        .HasColumnType("int");

                    b.Property<int>("BehaviorOfDoctor")
                        .HasColumnType("int");

                    b.Property<int>("BehaviorOfMedicalStaff")
                        .HasColumnType("int");

                    b.Property<int>("Cleanliness")
                        .HasColumnType("int");

                    b.Property<string>("DoctorJmbg")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<int>("DoctorProfessionalism")
                        .HasColumnType("int");

                    b.Property<int>("EaseInObtainingFollowupInformationAndCare")
                        .HasColumnType("int");

                    b.Property<int>("GettingAdviceByDoctor")
                        .HasColumnType("int");

                    b.Property<int>("GettingAdviceByMedicalStaff")
                        .HasColumnType("int");

                    b.Property<int>("MedicalStaffProfessionalism")
                        .HasColumnType("int");

                    b.Property<int>("Nursing")
                        .HasColumnType("int");

                    b.Property<int>("OverallRating")
                        .HasColumnType("int");

                    b.Property<int>("SatisfiedWithDrugAndInstrument")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DoctorJmbg");

                    b.ToTable("Surveys");
                });

            modelBuilder.Entity("Model.Manager.Drug", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("DrugTypeId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Producer")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DrugTypeId");

                    b.ToTable("Drugs");
                });

            modelBuilder.Entity("Model.Manager.DrugType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Purpose")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Type")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("DrugTypes");
                });

            modelBuilder.Entity("Model.Manager.Ingredient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("Alergen")
                        .HasColumnType("tinyint(1)");

                    b.Property<int?>("DrugId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("DrugId");

                    b.ToTable("Ingridients");
                });

            modelBuilder.Entity("Model.Manager.RenovationPeriod", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("BeginDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("RoomNumber")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoomNumber");

                    b.ToTable("RenovationPeriods");
                });

            modelBuilder.Entity("Model.Manager.Room", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.Property<int>("Occupation")
                        .HasColumnType("int");

                    b.Property<bool>("Renovation")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("Usage")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("Model.Manager.WorkingTime", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("DoctorJmbg")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("WorkShift")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DoctorJmbg");

                    b.ToTable("WorkingTimes");
                });

            modelBuilder.Entity("Model.NotificationSurveyAndFeedback.Feedback", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Comment")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("CommentatorJmbg")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<bool>("IsAllowedToPublish")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsPublished")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("SendingDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("CommentatorJmbg");

                    b.ToTable("Feedbacks");
                });

            modelBuilder.Entity("Model.PerformingExamination.Examination", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Anamnesis")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("DateAndTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("DoctorJmbg")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<int>("IdPatientCard")
                        .HasColumnType("int");

                    b.Property<int>("IdRoom")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DoctorJmbg");

                    b.HasIndex("IdPatientCard");

                    b.HasIndex("IdRoom");

                    b.ToTable("Examinations");
                });

            modelBuilder.Entity("Model.PerformingExamination.Therapy", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("DailyDose")
                        .HasColumnType("int");

                    b.Property<string>("Diagnosis")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("IdDrug")
                        .HasColumnType("int");

                    b.Property<int>("IdExamination")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("IdDrug");

                    b.HasIndex("IdExamination");

                    b.ToTable("Therapies");
                });

            modelBuilder.Entity("Model.Users.City", b =>
                {
                    b.Property<int>("ZipCode")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CountryId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("ZipCode");

                    b.HasIndex("CountryId");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("Model.Users.PatientCard", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Alergies")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("BloodType")
                        .HasColumnType("int");

                    b.Property<bool>("HasInsurance")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Lbo")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("MedicalHistory")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("PatientJmbg")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<int>("RhFactor")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PatientJmbg")
                        .IsUnique();

                    b.ToTable("PatientCards");
                });

            modelBuilder.Entity("Model.Users.User", b =>
                {
                    b.Property<string>("Jmbg")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<int>("CityZipCode")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Email")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<string>("HomeAddress")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Password")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Phone")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Surname")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Username")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Jmbg");

                    b.HasIndex("CityZipCode");

                    b.ToTable("User");

                    b.HasDiscriminator<string>("Discriminator").HasValue("User");
                });

            modelBuilder.Entity("Model.Users.Admin", b =>
                {
                    b.HasBaseType("Model.Users.User");

                    b.HasDiscriminator().HasValue("Admin");
                });

            modelBuilder.Entity("Model.Users.Doctor", b =>
                {
                    b.HasBaseType("Model.Users.User");

                    b.Property<DateTime>("DateOfEmployment")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("DoctorsOfficeId")
                        .HasColumnType("int");

                    b.Property<string>("NumberOfLicence")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasIndex("DoctorsOfficeId");

                    b.HasDiscriminator().HasValue("Doctor");
                });

            modelBuilder.Entity("Model.Users.Patient", b =>
                {
                    b.HasBaseType("Model.Users.User");

                    b.Property<DateTime>("DateOfRegistration")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ImageName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("IsActive")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsGuest")
                        .HasColumnType("tinyint(1)");

                    b.HasDiscriminator().HasValue("Patient");
                });

            modelBuilder.Entity("Backend.Model.ActionBenefit", b =>
                {
                    b.HasOne("Backend.Model.Pharmacies.Pharmacy", "Pharmacy")
                        .WithMany()
                        .HasForeignKey("PharmacyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Backend.Model.Survey", b =>
                {
                    b.HasOne("Model.Users.Doctor", "Doctor")
                        .WithMany("Surveys")
                        .HasForeignKey("DoctorJmbg");
                });

            modelBuilder.Entity("Model.Manager.Drug", b =>
                {
                    b.HasOne("Model.Manager.DrugType", "DrugType")
                        .WithMany()
                        .HasForeignKey("DrugTypeId");
                });

            modelBuilder.Entity("Model.Manager.Ingredient", b =>
                {
                    b.HasOne("Model.Manager.Drug", null)
                        .WithMany("Ingredient")
                        .HasForeignKey("DrugId");
                });

            modelBuilder.Entity("Model.Manager.RenovationPeriod", b =>
                {
                    b.HasOne("Model.Manager.Room", "Room")
                        .WithMany()
                        .HasForeignKey("RoomNumber")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Model.Manager.WorkingTime", b =>
                {
                    b.HasOne("Model.Users.Doctor", "Doctor")
                        .WithMany("WorkingTimes")
                        .HasForeignKey("DoctorJmbg");
                });

            modelBuilder.Entity("Model.NotificationSurveyAndFeedback.Feedback", b =>
                {
                    b.HasOne("Model.Users.Patient", "Commentator")
                        .WithMany("Feedbacks")
                        .HasForeignKey("CommentatorJmbg");
                });

            modelBuilder.Entity("Model.PerformingExamination.Examination", b =>
                {
                    b.HasOne("Model.Users.Doctor", "Doctor")
                        .WithMany()
                        .HasForeignKey("DoctorJmbg");

                    b.HasOne("Model.Users.PatientCard", "PatientCard")
                        .WithMany("Examinations")
                        .HasForeignKey("IdPatientCard")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Model.Manager.Room", "Room")
                        .WithMany()
                        .HasForeignKey("IdRoom")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Model.PerformingExamination.Therapy", b =>
                {
                    b.HasOne("Model.Manager.Drug", "Drug")
                        .WithMany()
                        .HasForeignKey("IdDrug")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Model.PerformingExamination.Examination", "Examination")
                        .WithMany("Therapies")
                        .HasForeignKey("IdExamination")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Model.Users.City", b =>
                {
                    b.HasOne("Backend.Country", "Country")
                        .WithMany("Cities")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Model.Users.PatientCard", b =>
                {
                    b.HasOne("Model.Users.Patient", "Patient")
                        .WithOne("PatientCard")
                        .HasForeignKey("Model.Users.PatientCard", "PatientJmbg");
                });

            modelBuilder.Entity("Model.Users.User", b =>
                {
                    b.HasOne("Model.Users.City", "City")
                        .WithMany("Users")
                        .HasForeignKey("CityZipCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Model.Users.Doctor", b =>
                {
                    b.HasOne("Model.Manager.Room", "DoctorsOffice")
                        .WithMany()
                        .HasForeignKey("DoctorsOfficeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
