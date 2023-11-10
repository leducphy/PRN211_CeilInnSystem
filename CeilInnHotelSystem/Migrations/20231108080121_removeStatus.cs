using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CeilInnHotelSystem.Migrations
{
    public partial class removeStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Rooms");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Rooms",
                type: "bit",
                nullable: true);
        }
    }
}
