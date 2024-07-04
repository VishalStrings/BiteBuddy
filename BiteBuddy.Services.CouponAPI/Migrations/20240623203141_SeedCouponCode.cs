using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BiteBuddy.Services.CouponAPI.Migrations
{
    public partial class SeedCouponCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Coupons",
                columns: new[] { "CouponCode", "CreationDate", "Description", "DiscountAmount", "ExpirationDate", "Id", "IsActive", "MinimumAmount", "ModifyDate" },
                values: new object[] { "100OFF", new DateTime(2024, 6, 24, 2, 1, 41, 40, DateTimeKind.Local).AddTicks(6072), "Rs 100 Off", 100m, new DateTime(2026, 6, 24, 2, 1, 41, 40, DateTimeKind.Local).AddTicks(6072), 2, true, 1000m, null });

            migrationBuilder.InsertData(
                table: "Coupons",
                columns: new[] { "CouponCode", "CreationDate", "Description", "DiscountAmount", "ExpirationDate", "Id", "IsActive", "MinimumAmount", "ModifyDate" },
                values: new object[] { "NEWUSER", new DateTime(2024, 6, 24, 2, 1, 41, 40, DateTimeKind.Local).AddTicks(6030), "Valid Only for New Users", 100m, new DateTime(2026, 6, 24, 2, 1, 41, 40, DateTimeKind.Local).AddTicks(6042), 1, true, 500m, null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Coupons",
                keyColumn: "CouponCode",
                keyValue: "100OFF");

            migrationBuilder.DeleteData(
                table: "Coupons",
                keyColumn: "CouponCode",
                keyValue: "NEWUSER");
        }
    }
}
