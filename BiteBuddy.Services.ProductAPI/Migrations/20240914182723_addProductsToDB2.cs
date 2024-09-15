using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BiteBuddy.Services.ProductAPI.Migrations
{
    public partial class addProductsToDB2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Products",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ProductCategories",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

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
                values: new object[] { 1, "C001", new DateTime(2024, 9, 14, 23, 57, 23, 318, DateTimeKind.Local).AddTicks(197), "Soft drink", null, null, "Coke", 1.5m, 1, new DateTime(2024, 9, 14, 23, 57, 23, 318, DateTimeKind.Local).AddTicks(208) });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Code", "CreatedDate", "Description", "ImageLocalPath", "ImageUrl", "Name", "Price", "ProductCategoryId", "UpdatedDate" },
                values: new object[] { 2, "S001", new DateTime(2024, 9, 14, 23, 57, 23, 318, DateTimeKind.Local).AddTicks(214), "Crispy snack", null, null, "Chips", 2.0m, 2, new DateTime(2024, 9, 14, 23, 57, 23, 318, DateTimeKind.Local).AddTicks(214) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Products",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ProductCategories",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);
        }
    }
}
