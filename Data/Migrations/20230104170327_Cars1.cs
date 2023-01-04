using Microsoft.EntityFrameworkCore.Migrations;

namespace RentCarsApp.Data.Migrations
{
    public partial class Cars1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Cars",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Cars");
        }
    }
}
