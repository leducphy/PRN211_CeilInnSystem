using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CeilInnHotelSystem.Migrations
{
    public partial class addDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateOccupied",
                table: "Occupancies",
                newName: "StartDate");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Occupancies",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Occupancies");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "Occupancies",
                newName: "DateOccupied");
        }
    }
}
