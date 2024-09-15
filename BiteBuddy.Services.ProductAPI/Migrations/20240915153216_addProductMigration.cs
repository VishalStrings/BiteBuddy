using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BiteBuddy.Services.ProductAPI.Migrations
{
    public partial class addProductMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageLocalPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductCategoryId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Products_ProductCategories_ProductCategoryId",
                        column: x => x.ProductCategoryId,
                        principalTable: "ProductCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ProductCategories",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 1, "Drinks and refreshments", "Beverages" });

            migrationBuilder.InsertData(
                table: "ProductCategories",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 2, "Light food items", "Snacks" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Code", "CreatedDate", "Description", "ImageLocalPath", "ImageUrl", "Name", "Price", "ProductCategoryId", "UpdatedDate" },
                values: new object[] { 1, "C001", new DateTime(2024, 9, 15, 21, 2, 15, 894, DateTimeKind.Local).AddTicks(8453), "Soft drink", null, null, "Coke", 1.5m, 1, new DateTime(2024, 9, 15, 21, 2, 15, 894, DateTimeKind.Local).AddTicks(8462) });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Code", "CreatedDate", "Description", "ImageLocalPath", "ImageUrl", "Name", "Price", "ProductCategoryId", "UpdatedDate" },
                values: new object[] { 2, "S001", new DateTime(2024, 9, 15, 21, 2, 15, 894, DateTimeKind.Local).AddTicks(8467), "Crispy snack", null, null, "Chips", 2.0m, 2, new DateTime(2024, 9, 15, 21, 2, 15, 894, DateTimeKind.Local).AddTicks(8468) });

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductCategoryId",
                table: "Products",
                column: "ProductCategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "ProductCategories");
        }
    }
}
