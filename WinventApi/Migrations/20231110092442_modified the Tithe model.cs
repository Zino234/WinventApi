using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WinventApi.Migrations
{
    public partial class modifiedtheTithemodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "TitheAmount",
                table: "Tithes",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateTable(
                name: "TransportSeeds",
                columns: table => new
                {
                    TransportSeedId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TransportSeedName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransportSeedCollectedAt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransportSeedGivenBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransportSeedAmount = table.Column<double>(type: "float", nullable: false),
                    TransportSeedCollectedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransportSeedCreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OfficerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransportSeeds", x => x.TransportSeedId);
                    table.ForeignKey(
                        name: "FK_TransportSeeds_Officers_OfficerId",
                        column: x => x.OfficerId,
                        principalTable: "Officers",
                        principalColumn: "OficcerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TransportSeeds_OfficerId",
                table: "TransportSeeds",
                column: "OfficerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransportSeeds");

            migrationBuilder.DropColumn(
                name: "TitheAmount",
                table: "Tithes");
        }
    }
}
