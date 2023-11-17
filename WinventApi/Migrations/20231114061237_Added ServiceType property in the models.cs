using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WinventApi.Migrations
{
    public partial class AddedServiceTypepropertyinthemodels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ServiceType",
                table: "TransportSeeds",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ServiceType",
                table: "Tithes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ServiceType",
                table: "Offerings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ServiceType",
                table: "Expenses",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ServiceType",
                table: "TransportSeeds");

            migrationBuilder.DropColumn(
                name: "ServiceType",
                table: "Tithes");

            migrationBuilder.DropColumn(
                name: "ServiceType",
                table: "Offerings");

            migrationBuilder.DropColumn(
                name: "ServiceType",
                table: "Expenses");
        }
    }
}
