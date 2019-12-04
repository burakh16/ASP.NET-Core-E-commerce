using Microsoft.EntityFrameworkCore.Migrations;

namespace ecommerce.Migrations
{
    public partial class Product2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Products_ProductOptionId",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "Option",
                table: "ProductOption",
                newName: "POption");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductOptionId",
                table: "Products",
                column: "ProductOptionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Products_ProductOptionId",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "POption",
                table: "ProductOption",
                newName: "Option");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductOptionId",
                table: "Products",
                column: "ProductOptionId",
                unique: true);
        }
    }
}
