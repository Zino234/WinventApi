using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WinventApi.Migrations
{
    public partial class Initials : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    AdminId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AdminFirstname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdminLastname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdminUsername = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdminPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdminEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdminPassword = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.AdminId);
                });

            migrationBuilder.CreateTable(
                name: "Officers",
                columns: table => new
                {
                    OficcerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OfficerFirstname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OfficerLastname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OfficerUsername = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OfficerPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OfficerEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OfficerPassword = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OfficerIsDisabled = table.Column<bool>(type: "bit", nullable: false),
                    OfficerCreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Officers", x => x.OficcerId);
                });

            migrationBuilder.CreateTable(
                name: "Expenses",
                columns: table => new
                {
                    ExpenseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExpenseTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpenseDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpenseAmount = table.Column<float>(type: "real", nullable: false),
                    ExpenseDoneAt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpenseCreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpenseDoneBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OfficerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expenses", x => x.ExpenseId);
                    table.ForeignKey(
                        name: "FK_Expenses_Officers_OfficerId",
                        column: x => x.OfficerId,
                        principalTable: "Officers",
                        principalColumn: "OficcerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Offerings",
                columns: table => new
                {
                    OfferingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OfferingAmount = table.Column<double>(type: "float", nullable: false),
                    OfferingCollectedAt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OfferingCollectedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OfferingcreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OfficerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offerings", x => x.OfferingId);
                    table.ForeignKey(
                        name: "FK_Offerings_Officers_OfficerId",
                        column: x => x.OfficerId,
                        principalTable: "Officers",
                        principalColumn: "OficcerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tithes",
                columns: table => new
                {
                    TitheId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TitheName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TitheCollectedAt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TitheGivenBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TitheCollectedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TitheCreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OfficerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tithes", x => x.TitheId);
                    table.ForeignKey(
                        name: "FK_Tithes_Officers_OfficerId",
                        column: x => x.OfficerId,
                        principalTable: "Officers",
                        principalColumn: "OficcerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_OfficerId",
                table: "Expenses",
                column: "OfficerId");

            migrationBuilder.CreateIndex(
                name: "IX_Offerings_OfficerId",
                table: "Offerings",
                column: "OfficerId");

            migrationBuilder.CreateIndex(
                name: "IX_Tithes_OfficerId",
                table: "Tithes",
                column: "OfficerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "Expenses");

            migrationBuilder.DropTable(
                name: "Offerings");

            migrationBuilder.DropTable(
                name: "Tithes");

            migrationBuilder.DropTable(
                name: "Officers");
        }
    }
}
