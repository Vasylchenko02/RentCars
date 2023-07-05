using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentCarsApp.Data.Migrations
{
    public partial class modelname : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Cars",
                newName: "NameProducer");

            migrationBuilder.AddColumn<string>(
                name: "NameModel",
                table: "Cars",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NameModel",
                table: "Cars");

            migrationBuilder.RenameColumn(
                name: "NameProducer",
                table: "Cars",
                newName: "Name");
        }
    }
}
