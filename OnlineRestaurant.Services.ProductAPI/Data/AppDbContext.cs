using Microsoft.EntityFrameworkCore;
using OnlineRestaurant.Services.ProductAPI.Models;

namespace OnlineRestaurant.Services.ProductAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 1,
                Name = "Pizza",
                Description = "Delicious cheese pizza",
                Price = 9.99m,
                CategoryName = "Fast Food",
                ImageUrl = "https://fastly.picsum.photos/id/64/4326/2884.jpg?hmac=9_SzX666YRpR_fOyYStXpfSiJ_edO3ghlSRnH2w09Kg"
            },
            new Product
            {
                Id = 2,
                Name = "Burger",
                Description = "Juicy beef burger with lettuce and tomato",
                Price = 5.99m,
                CategoryName = "Fast Food",
                ImageUrl = "https://fastly.picsum.photos/id/65/4912/3264.jpg?hmac=uq0IxYtPIqRKinGruj45KcPPzxDjQvErcxyS1tn7bG0"
            },
            new Product
            {
                Id = 3,
                Name = "Pasta",
                Description = "Creamy Alfredo pasta with chicken",
                Price = 12.99m,
                CategoryName = "Italian",
                ImageUrl = "https://fastly.picsum.photos/id/325/4928/3264.jpg?hmac=D_X6AKqCcH8IpWElX5X3dxx11yn7yYO-vPhiKhzRbwI"
            },
            new Product
            {
                Id = 4,
                Name = "Salad",
                Description = "Fresh garden salad with vinaigrette dressing",
                Price = 7.99m,
                CategoryName = "Healthy",
                ImageUrl = "https://fastly.picsum.photos/id/230/1500/1500.jpg?hmac=heg53PqHqX88fhXrDyqlqJK8lLJXGRudsOXMKB3BZtc"
            });
        }
    }
}
    