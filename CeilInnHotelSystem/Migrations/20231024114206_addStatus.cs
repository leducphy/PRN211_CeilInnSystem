using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CeilInnHotelSystem.Migrations
{
    public partial class addStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Rooms",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Employees",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Customers",
                type: "bit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Customers");
        }
    }
}
