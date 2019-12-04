using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ecommerce.Migrations
{
    public partial class cart2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductOptions_ProductOptionId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "ProductOptions");

            migrationBuilder.DropIndex(
                name: "IX_Products_ProductOptionId",
                table: "Products");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductOptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    POption = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductOptions", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductOptionId",
                table: "Products",
                column: "ProductOptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductOptions_ProductOptionId",
                table: "Products",
                column: "ProductOptionId",
                principalTable: "ProductOptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
