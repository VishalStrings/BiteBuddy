using Microsoft.EntityFrameworkCore;
using BiteBuddy.Services.CouponAPI.Models;

namespace BiteBuddy.Services.CouponAPI.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }

        public DbSet<Coupon> Coupons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Coupon>().HasData(new Coupon
            {
                Id = 1,
                CouponCode = "NEWUSER",
                DiscountAmount = 100,
                MinimumAmount = 500,
                CreationDate = DateTime.Now,
                ExpirationDate = DateTime.Now.AddYears(2),
                IsActive = true,
                Description = "Valid Only for New Users"
            });

            modelBuilder.Entity<Coupon>().HasData(new Coupon
            {
                Id = 2,
                CouponCode = "100OFF",
                DiscountAmount = 100,
                MinimumAmount = 1000,
                CreationDate = DateTime.Now,
                ExpirationDate = DateTime.Now.AddYears(2),
                IsActive = true,
                Description = "Rs 100 Off"
            });
        }
    }
}
