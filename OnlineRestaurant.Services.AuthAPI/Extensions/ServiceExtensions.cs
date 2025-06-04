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
        public static IServiceCollection RegisterServices(this IServiceCollection services,WebApplicationBuilder builder)
        {
            services.AddDbContext<AppDbContext>(options =>
             options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

            services.Configure<JwtOptions>(builder.Configuration.GetSection("ApiConfigs:JwtOptions"));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();
            return services;
        }   
    }
}
