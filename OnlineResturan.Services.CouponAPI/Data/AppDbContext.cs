using Microsoft.EntityFrameworkCore;
using OnlineRestaurant.Services.CouponAPI.Models;

namespace OnlineRestaurant.Services.CouponAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Coupon> Coupons { get; set; } = null!;
       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Coupon>().HasData(
                new Coupon
                {
                    Id = 1,
                    CouponCode = "10OFF",
                    DiscountAmount = 10,
                    MinAmount = 20
                },
                new Coupon
                {
                    Id = 2,
                    CouponCode = "20OFF",
                    DiscountAmount = 20,
                    MinAmount = 50
                }
            );
            base.OnModelCreating(modelBuilder);
        }
    }

}
