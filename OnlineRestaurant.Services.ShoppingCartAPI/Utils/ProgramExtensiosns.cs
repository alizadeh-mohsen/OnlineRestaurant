using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OnlineRestaurant.Services.ShoppingCartAPI.Data;
using OnlineRestaurant.Services.ShoppingCartAPI.Service;
using OnlineRestaurant.Services.ShoppingCartAPI.Service.IService;
using OnlineRestaurant.Services.ShoppingCartAPI.Utils;
using System.Text;

namespace OnlineRestaurant.Services.ProductAPI.Utils
{
    public static class ProgramExtensions
    {
        public static WebApplicationBuilder AddCustomServices(this WebApplicationBuilder builder)
        {

            builder.Services.AddHttpContextAccessor();
            builder.Services.AddScoped<AuthMiddleware>();
            
            builder.Services.AddHttpClient<IProductService, ProductService>().AddHttpMessageHandler<AuthMiddleware>();
            builder.Services.AddHttpClient<ICouponService, CouponService>().AddHttpMessageHandler<AuthMiddleware>() ;

            builder.Services.AddAutoMapper(typeof(MappingProfiles).Assembly);

            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            var serviceUrls = builder.Configuration.GetSection("ServiceUrls");
            Helper.ProductBaseApiUrl = serviceUrls.GetValue<string>("ProductBaseApiUrl") ?? string.Empty;
            Helper.CouponBaseApiUrl = serviceUrls.GetValue<string>("CouponBaseApiUrl") ?? string.Empty;

            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IBaseService, BaseService>();
            builder.Services.AddScoped<ICouponService, CouponService>();


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
