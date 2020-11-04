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
                name: "Cities",
                columns: table => new
                {
                    ZipCode = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.ZipCode);
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
                    DateOfRegistration = table.Column<DateTime>(nullable: true)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IsGuest = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Jmbg);
                    table.ForeignKey(
                        name: "FK_User_Cities_CityZipCode",
                        column: x => x.CityZipCode,
                        principalTable: "Cities",
                        principalColumn: "ZipCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Feedbacks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
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

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "ZipCode", "Name" },
                values: new object[] { 21000, "Novi Sad" });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Jmbg", "CityZipCode", "DateOfBirth", "Discriminator", "Email", "Gender", "HomeAddress", "Name", "Password", "Phone", "Surname", "Username" },
                values: new object[] { "0606960851265", 21000, new DateTime(1960, 6, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", "", 0, "", "Pera", "123", "", "Perić", "pera" });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Jmbg", "CityZipCode", "DateOfBirth", "Discriminator", "Email", "Gender", "HomeAddress", "Name", "Password", "Phone", "Surname", "Username", "DateOfRegistration", "IsGuest" },
                values: new object[] { "2305992104895", 21000, new DateTime(1992, 5, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "Patient", "", 0, "", "Marko", "123", "", "Marković", "marko", new DateTime(2020, 11, 4, 17, 48, 36, 719, DateTimeKind.Local).AddTicks(2362), true });

            migrationBuilder.InsertData(
                table: "Feedbacks",
                columns: new[] { "Id", "Comment", "CommentatorJmbg", "IsAllowedToPublish", "IsPublished" },
                values: new object[] { 1, "Zadovoljan sam uslugama bolnice.", "2305992104895", true, true });

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_CommentatorJmbg",
                table: "Feedbacks",
                column: "CommentatorJmbg");

            migrationBuilder.CreateIndex(
                name: "IX_User_CityZipCode",
                table: "User",
                column: "CityZipCode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Feedbacks");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Cities");
        }
    }
}
