using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TripLog.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Trips",
                columns: table => new
                {
                    TripId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Destination = table.Column<string>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    Accommodation = table.Column<string>(nullable: true),
                    AccommodationPhone = table.Column<string>(nullable: true),
                    AccommodationEmail = table.Column<string>(nullable: true),
                    ThingToDo1 = table.Column<string>(nullable: true),
                    ThingToDo2 = table.Column<string>(nullable: true),
                    ThingToDo3 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trips", x => x.TripId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Trips");
        }
    }
}
