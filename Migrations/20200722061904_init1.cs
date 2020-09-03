using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ParkingSystemCore.Migrations
{
    public partial class init1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Parkingslots",
                columns: table => new
                {
                    sl = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    floor = table.Column<int>(nullable: false),
                    slot = table.Column<int>(nullable: false),
                    section = table.Column<string>(nullable: false),
                    availability = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parkingslots", x => x.sl);
                });

            migrationBuilder.CreateTable(
                name: "Backups",
                columns: table => new
                {
                    sl = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    vehicleNo = table.Column<string>(nullable: true),
                    name = table.Column<string>(nullable: true),
                    contactNumber = table.Column<string>(nullable: true),
                    inTime = table.Column<double>(nullable: false),
                    outTime = table.Column<double>(nullable: false),
                    cost = table.Column<double>(nullable: false),
                    slot = table.Column<int>(nullable: false),
                    date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Backups", x => x.sl);
                    table.ForeignKey(
                        name: "FK_Backups_Parkingslots_slot",
                        column: x => x.slot,
                        principalTable: "Parkingslots",
                        principalColumn: "sl",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Details",
                columns: table => new
                {
                    vehicleNo = table.Column<string>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    contactNumber = table.Column<string>(nullable: false),
                    date = table.Column<DateTime>(nullable: false),
                    inTime = table.Column<double>(nullable: false),
                    outTime = table.Column<double>(nullable: true),
                    cost = table.Column<double>(nullable: true),
                    Slot = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Details", x => x.vehicleNo);
                    table.ForeignKey(
                        name: "FK_Details_Parkingslots_Slot",
                        column: x => x.Slot,
                        principalTable: "Parkingslots",
                        principalColumn: "sl",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Backups_slot",
                table: "Backups",
                column: "slot");

            migrationBuilder.CreateIndex(
                name: "IX_Details_Slot",
                table: "Details",
                column: "Slot");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Backups");

            migrationBuilder.DropTable(
                name: "Details");

            migrationBuilder.DropTable(
                name: "Parkingslots");
        }
    }
}
