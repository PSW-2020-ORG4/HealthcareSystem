using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Backend.Migrations
{
    public partial class FirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DrugTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Type = table.Column<string>(nullable: true),
                    Purpose = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DrugTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pharmacies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    ApiKey = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pharmacies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Usage = table.Column<int>(nullable: false),
                    Capacity = table.Column<int>(nullable: false),
                    Occupation = table.Column<int>(nullable: false),
                    Renovation = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    ZipCode = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    CountryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.ZipCode);
                    table.ForeignKey(
                        name: "FK_Cities_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Drugs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DrugTypeId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Quantity = table.Column<int>(nullable: false),
                    ExpirationDate = table.Column<DateTime>(nullable: false),
                    Producer = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drugs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Drugs_DrugTypes_DrugTypeId",
                        column: x => x.DrugTypeId,
                        principalTable: "DrugTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RenovationPeriods",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RoomNumber = table.Column<int>(nullable: false),
                    BeginDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RenovationPeriods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RenovationPeriods_Rooms_RoomNumber",
                        column: x => x.RoomNumber,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Jmbg = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Surname = table.Column<string>(nullable: true),
                    Gender = table.Column<int>(nullable: false),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    Phone = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    CityZipCode = table.Column<int>(nullable: false),
                    HomeAddress = table.Column<string>(nullable: true),
                    Username = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false),
                    NumberOfLicence = table.Column<string>(nullable: true),
                    Type = table.Column<int>(nullable: true),
                    DateOfEmployment = table.Column<DateTime>(nullable: true),
                    DoctorsOfficeId = table.Column<int>(nullable: true),
                    DateOfRegistration = table.Column<DateTime>(nullable: true)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IsGuest = table.Column<bool>(nullable: true),
                    IsActive = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Jmbg);
                    table.ForeignKey(
                        name: "FK_User_Rooms_DoctorsOfficeId",
                        column: x => x.DoctorsOfficeId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_User_Cities_CityZipCode",
                        column: x => x.CityZipCode,
                        principalTable: "Cities",
                        principalColumn: "ZipCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ingridients",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Alergen = table.Column<bool>(nullable: false),
                    DrugId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingridients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ingridients_Drugs_DrugId",
                        column: x => x.DrugId,
                        principalTable: "Drugs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Feedbacks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SendingDate = table.Column<DateTime>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Comment = table.Column<string>(nullable: true),
                    IsPublished = table.Column<bool>(nullable: false),
                    IsAllowedToPublish = table.Column<bool>(nullable: false),
                    CommentatorJmbg = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedbacks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Feedbacks_User_CommentatorJmbg",
                        column: x => x.CommentatorJmbg,
                        principalTable: "User",
                        principalColumn: "Jmbg",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PatientCards",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    BloodType = table.Column<int>(nullable: false),
                    RhFactor = table.Column<int>(nullable: false),
                    Alergies = table.Column<string>(nullable: true),
                    MedicalHistory = table.Column<string>(nullable: true),
                    HasInsurance = table.Column<bool>(nullable: false),
                    Lbo = table.Column<string>(nullable: true),
                    PatientJmbg = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientCards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientCards_User_PatientJmbg",
                        column: x => x.PatientJmbg,
                        principalTable: "User",
                        principalColumn: "Jmbg",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Surveys",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    BehaviorOfDoctor = table.Column<int>(nullable: false),
                    DoctorProfessionalism = table.Column<int>(nullable: false),
                    GettingAdviceByDoctor = table.Column<int>(nullable: false),
                    AvailabilityOfDoctor = table.Column<int>(nullable: false),
                    BehaviorOfMedicalStaff = table.Column<int>(nullable: false),
                    MedicalStaffProfessionalism = table.Column<int>(nullable: false),
                    GettingAdviceByMedicalStaff = table.Column<int>(nullable: false),
                    EaseInObtainingFollowupInformationAndCare = table.Column<int>(nullable: false),
                    Nursing = table.Column<int>(nullable: false),
                    Cleanliness = table.Column<int>(nullable: false),
                    OverallRating = table.Column<int>(nullable: false),
                    SatisfiedWithDrugAndInstrument = table.Column<int>(nullable: false),
                    DoctorJmbg = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Surveys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Surveys_User_DoctorJmbg",
                        column: x => x.DoctorJmbg,
                        principalTable: "User",
                        principalColumn: "Jmbg",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WorkingTimes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DoctorJmbg = table.Column<string>(nullable: true),
                    WorkShift = table.Column<int>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkingTimes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkingTimes_User_DoctorJmbg",
                        column: x => x.DoctorJmbg,
                        principalTable: "User",
                        principalColumn: "Jmbg",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Examinations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Type = table.Column<int>(nullable: false),
                    DateAndTime = table.Column<DateTime>(nullable: false),
                    DoctorJmbg = table.Column<string>(nullable: true),
                    IdRoom = table.Column<int>(nullable: false),
                    IdPatientCard = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Examinations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Examinations_User_DoctorJmbg",
                        column: x => x.DoctorJmbg,
                        principalTable: "User",
                        principalColumn: "Jmbg",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Examinations_PatientCards_IdPatientCard",
                        column: x => x.IdPatientCard,
                        principalTable: "PatientCards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Examinations_Rooms_IdRoom",
                        column: x => x.IdRoom,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Therapies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Anamnesis = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    DailyDose = table.Column<int>(nullable: false),
                    IdDrug = table.Column<int>(nullable: false),
                    IdExamination = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Therapies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Therapies_Drugs_IdDrug",
                        column: x => x.IdDrug,
                        principalTable: "Drugs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Therapies_Examinations_IdExamination",
                        column: x => x.IdExamination,
                        principalTable: "Examinations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cities_CountryId",
                table: "Cities",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Drugs_DrugTypeId",
                table: "Drugs",
                column: "DrugTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Examinations_DoctorJmbg",
                table: "Examinations",
                column: "DoctorJmbg");

            migrationBuilder.CreateIndex(
                name: "IX_Examinations_IdPatientCard",
                table: "Examinations",
                column: "IdPatientCard");

            migrationBuilder.CreateIndex(
                name: "IX_Examinations_IdRoom",
                table: "Examinations",
                column: "IdRoom");

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_CommentatorJmbg",
                table: "Feedbacks",
                column: "CommentatorJmbg");

            migrationBuilder.CreateIndex(
                name: "IX_Ingridients_DrugId",
                table: "Ingridients",
                column: "DrugId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientCards_PatientJmbg",
                table: "PatientCards",
                column: "PatientJmbg",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RenovationPeriods_RoomNumber",
                table: "RenovationPeriods",
                column: "RoomNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Surveys_DoctorJmbg",
                table: "Surveys",
                column: "DoctorJmbg");

            migrationBuilder.CreateIndex(
                name: "IX_Therapies_IdDrug",
                table: "Therapies",
                column: "IdDrug");

            migrationBuilder.CreateIndex(
                name: "IX_Therapies_IdExamination",
                table: "Therapies",
                column: "IdExamination");

            migrationBuilder.CreateIndex(
                name: "IX_User_DoctorsOfficeId",
                table: "User",
                column: "DoctorsOfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_User_CityZipCode",
                table: "User",
                column: "CityZipCode");

            migrationBuilder.CreateIndex(
                name: "IX_WorkingTimes_DoctorJmbg",
                table: "WorkingTimes",
                column: "DoctorJmbg");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Feedbacks");

            migrationBuilder.DropTable(
                name: "Ingridients");

            migrationBuilder.DropTable(
                name: "Pharmacies");

            migrationBuilder.DropTable(
                name: "RenovationPeriods");

            migrationBuilder.DropTable(
                name: "Surveys");

            migrationBuilder.DropTable(
                name: "Therapies");

            migrationBuilder.DropTable(
                name: "WorkingTimes");

            migrationBuilder.DropTable(
                name: "Drugs");

            migrationBuilder.DropTable(
                name: "Examinations");

            migrationBuilder.DropTable(
                name: "DrugTypes");

            migrationBuilder.DropTable(
                name: "PatientCards");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}
