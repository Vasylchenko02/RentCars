using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentCarsApp.Data.Migrations
{
    public partial class gal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GalleryImage_Cars_CarId",
                table: "GalleryImage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GalleryImage",
                table: "GalleryImage");

            migrationBuilder.RenameTable(
                name: "GalleryImage",
                newName: "GalleryImages");

            migrationBuilder.RenameIndex(
                name: "IX_GalleryImage_CarId",
                table: "GalleryImages",
                newName: "IX_GalleryImages_CarId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GalleryImages",
                table: "GalleryImages",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GalleryImages_Cars_CarId",
                table: "GalleryImages",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GalleryImages_Cars_CarId",
                table: "GalleryImages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GalleryImages",
                table: "GalleryImages");

            migrationBuilder.RenameTable(
                name: "GalleryImages",
                newName: "GalleryImage");

            migrationBuilder.RenameIndex(
                name: "IX_GalleryImages_CarId",
                table: "GalleryImage",
                newName: "IX_GalleryImage_CarId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GalleryImage",
                table: "GalleryImage",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GalleryImage_Cars_CarId",
                table: "GalleryImage",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id");
        }
    }
}
