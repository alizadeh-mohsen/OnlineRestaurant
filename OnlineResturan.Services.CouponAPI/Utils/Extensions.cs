using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OnlineRestaurant.Services.CouponAPI.AutoMapper;
using OnlineRestaurant.Services.CouponAPI.Data;
using System.Text;

namespace OnlineRestaurant.Services.CouponAPI.Utils
{
    public static class Extensions
    {
        public static WebApplicationBuilder AddCustomServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddAutoMapper(typeof(MappingProfiles).Assembly);

            builder.Services.AddDbContext<AppDbContext>(
                options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            var jwtOptions = builder.Configuration.GetSection("JwtOptions");

            var secret = jwtOptions.GetValue<string>("Secret");
            var issuer = jwtOptions.GetValue<string>("Issuer");
            var audience = jwtOptions.GetValue<string>("Audience");

            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)),
                    ValidIssuer = issuer,
                    ValidAudience = audience
                };
            });

            builder.Services.AddAuthorization();

            return builder;
        }
    }
}
