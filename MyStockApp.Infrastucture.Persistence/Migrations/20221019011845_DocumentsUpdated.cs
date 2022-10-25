using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyStockApp.Infrastucture.Persistence.Migrations
{
    public partial class DocumentsUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Documents_Products_UserId",
                table: "Documents");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Documents",
                newName: "ProductID");

            migrationBuilder.RenameIndex(
                name: "IX_Documents_UserId",
                table: "Documents",
                newName: "IX_Documents_ProductID");

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_Products_ProductID",
                table: "Documents",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Documents_Products_ProductID",
                table: "Documents");

            migrationBuilder.RenameColumn(
                name: "ProductID",
                table: "Documents",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Documents_ProductID",
                table: "Documents",
                newName: "IX_Documents_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_Products_UserId",
                table: "Documents",
                column: "UserId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
