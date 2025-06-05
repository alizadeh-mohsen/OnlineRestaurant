using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineRestaurant.Services.AuthAPI.Models;

namespace OnlineRestaurant.Services.AuthAPI.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed roles
            var adminRoleId = "1";
            var customerRoleId = "2";
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = adminRoleId,
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Id = customerRoleId,
                    Name = "Customer",
                    NormalizedName = "CUSTOMER"
                }
            );

            // Seed admin user
            var adminUserId = "1";
            var hasher = new PasswordHasher<ApplicationUser>();
            var adminUser = new ApplicationUser
            {
                Id = adminUserId,
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@restaurant.com",
                NormalizedEmail = "ADMIN@RESTAURANT.COM",
                Name = "Admin User",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = string.Empty,
                PasswordHash = hasher.HashPassword(null, "Admin@123") // Set a default password
            };
            modelBuilder.Entity<ApplicationUser>().HasData(adminUser);

            // Assign admin user to admin role
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    UserId = adminUserId,
                    RoleId = adminRoleId
                }
            );
        }

    }
}
