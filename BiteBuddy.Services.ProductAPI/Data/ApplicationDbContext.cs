using Microsoft.EntityFrameworkCore;
using BiteBuddy.Services.ProductAPI.Models;

namespace BiteBuddy.Services.ProductAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {}

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure ProductCategory entity
            modelBuilder.Entity<ProductCategory>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<ProductCategory>()
                .Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<ProductCategory>()
                .HasMany(c => c.Products)
                .WithOne(p => p.ProductCategory)
                .HasForeignKey(p => p.ProductCategoryId);

            // Seed initial data
            modelBuilder.Entity<ProductCategory>().HasData(
                new ProductCategory { Id = 1, Name = "Beverages", Description = "Drinks and refreshments" },
                new ProductCategory { Id = 2, Name = "Snacks", Description = "Light food items" }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    ProductId = 1,
                    Name = "Coke",
                    Code = "C001",  // Provide a value for the required Code property
                    Price = 1.5M,
                    Description = "Soft drink",
                    ProductCategoryId = 1
                },
                new Product
                {
                    ProductId = 2,
                    Name = "Chips",
                    Code = "S001",  // Provide a value for the required Code property
                    Price = 2.0M,
                    Description = "Crispy snack",
                    ProductCategoryId = 2
                }
            );
        }

    }

}

