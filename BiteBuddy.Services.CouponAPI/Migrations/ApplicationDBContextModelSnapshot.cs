﻿// <auto-generated />
using System;
using BiteBuddy.Services.CouponAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BiteBuddy.Services.CouponAPI.Migrations
{
    [DbContext(typeof(ApplicationDBContext))]
    partial class ApplicationDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.31")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("BiteBuddy.Services.CouponAPI.Models.Coupon", b =>
                {
                    b.Property<string>("CouponCode")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<decimal>("DiscountAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime?>("ExpirationDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<decimal>("MinimumAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime?>("ModifyDate")
                        .HasColumnType("datetime2");

                    b.HasKey("CouponCode");

                    b.ToTable("Coupons");

                    b.HasData(
                        new
                        {
                            CouponCode = "NEWUSER",
                            CreationDate = new DateTime(2024, 6, 24, 2, 1, 41, 40, DateTimeKind.Local).AddTicks(6030),
                            Description = "Valid Only for New Users",
                            DiscountAmount = 100m,
                            ExpirationDate = new DateTime(2026, 6, 24, 2, 1, 41, 40, DateTimeKind.Local).AddTicks(6042),
                            Id = 1,
                            IsActive = true,
                            MinimumAmount = 500m
                        },
                        new
                        {
                            CouponCode = "100OFF",
                            CreationDate = new DateTime(2024, 6, 24, 2, 1, 41, 40, DateTimeKind.Local).AddTicks(6072),
                            Description = "Rs 100 Off",
                            DiscountAmount = 100m,
                            ExpirationDate = new DateTime(2026, 6, 24, 2, 1, 41, 40, DateTimeKind.Local).AddTicks(6072),
                            Id = 2,
                            IsActive = true,
                            MinimumAmount = 1000m
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
