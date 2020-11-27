using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Backend.Migrations
{
    public partial class NMGE : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Pharmacies",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "ApiKey",
                table: "Pharmacies",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "ActionsBenefitsExchangeName",
                table: "Pharmacies",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ActionsBenefitsSubscribed",
                table: "Pharmacies",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Pharmacies",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "ActionsBenefits",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PharmacyId = table.Column<int>(nullable: false),
                    Subject = table.Column<string>(nullable: false),
                    Message = table.Column<string>(nullable: false),
                    IsPublic = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActionsBenefits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActionsBenefits_Pharmacies_PharmacyId",
                        column: x => x.PharmacyId,
                        principalTable: "Pharmacies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ConsumableEquipments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Quantity = table.Column<int>(nullable: false),
                    Type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsumableEquipments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EquipmentsInRooms",
                columns: table => new
                {
                    IdEquipment = table.Column<int>(nullable: false),
                    RoomNumber = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentsInRooms", x => new { x.RoomNumber, x.IdEquipment });
                });

            migrationBuilder.CreateTable(
                name: "NonConsumableEquipments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NonConsumableEquipments", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pharmacies_ActionsBenefitsExchangeName",
                table: "Pharmacies",
                column: "ActionsBenefitsExchangeName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ActionsBenefits_PharmacyId",
                table: "ActionsBenefits",
                column: "PharmacyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActionsBenefits");

            migrationBuilder.DropTable(
                name: "ConsumableEquipments");

            migrationBuilder.DropTable(
                name: "EquipmentsInRooms");

            migrationBuilder.DropTable(
                name: "NonConsumableEquipments");

            migrationBuilder.DropIndex(
                name: "IX_Pharmacies_ActionsBenefitsExchangeName",
                table: "Pharmacies");

            migrationBuilder.DropColumn(
                name: "ActionsBenefitsExchangeName",
                table: "Pharmacies");

            migrationBuilder.DropColumn(
                name: "ActionsBenefitsSubscribed",
                table: "Pharmacies");

            migrationBuilder.DropColumn(
                name: "Url",
                table: "Pharmacies");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Pharmacies",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "ApiKey",
                table: "Pharmacies",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 255);
        }
    }
}
