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
                ImageUrl = "https://picsum.photos/seed/picsum/200/300"
            },
            new Product
            {
                Id = 2,
                Name = "Burger",
                Description = "Juicy beef burger with lettuce and tomato",
                Price = 5.99m,
                CategoryName = "Fast Food",
                ImageUrl = "https://picsum.photos/seed/picsum/200/300"
            },
            new Product
            {
                Id = 3,
                Name = "Pasta",
                Description = "Creamy Alfredo pasta with chicken",
                Price = 12.99m,
                CategoryName = "Italian",
                ImageUrl = "https://picsum.photos/seed/picsum/200/300"
            },
            new Product
            {
                Id = 4,
                Name = "Salad",
                Description = "Fresh garden salad with vinaigrette dressing",
                Price = 7.99m,
                CategoryName = "Healthy",
                ImageUrl = "https://picsum.photos/seed/picsum/200/300"
            });
        }
    }
}
