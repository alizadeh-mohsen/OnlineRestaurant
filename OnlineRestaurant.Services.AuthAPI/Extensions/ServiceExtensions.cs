using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineRestaurant.Services.AuthAPI.Data;
using OnlineRestaurant.Services.AuthAPI.Models;
using OnlineRestaurant.Services.AuthAPI.Services;
using OnlineRestaurant.Services.AuthAPI.Services.IService;

namespace OnlineRestaurant.Services.AuthAPI.Extensions
{
    public static class ServiceExtensions
    {
        public static WebApplicationBuilder RegisterCustomServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<AppDbContext>(options =>
             options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

            builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("JwtOptions"));

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();
            return builder;
        }
    }
}
