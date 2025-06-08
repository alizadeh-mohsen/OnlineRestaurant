 using Microsoft.AspNetCore.Authentication.Cookies;
using OnlineRestaurant.MVC.AutoMapper;
using OnlineRestaurant.MVC.Service;
using OnlineRestaurant.MVC.Service.IService;

namespace OnlineRestaurant.MVC.Utils
{
    public static class ServiceCollectionExtension
    {
        public static WebApplicationBuilder RegisterServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddHttpClient();

            builder.Services.AddHttpClient<ICouponService, CouponService>();
            builder.Services.AddHttpClient<IAuthService, AuthService>();
            builder.Services.AddHttpClient<IProductService, ProductService>();
            builder.Services.AddHttpClient<IShoppingCartService, ShoppingCartService>();

            builder.Services.AddScoped<IBaseService, BaseService>();
            builder.Services.AddScoped<ITokenProvider, TokenProvider>();
            builder.Services.AddScoped<ICouponService, CouponService>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IShoppingCartService, ShoppingCartService>();


            var serviceUrls = builder.Configuration.GetSection("ServiceUrls");
            Helper.AuthBaseApiUrl = serviceUrls.GetValue<string>("AuthAPI");
            Helper.CouponBaseApiUrl = serviceUrls.GetValue<string>("CouponAPI");
            Helper.ProductBaseApiUrl = serviceUrls.GetValue<string>("ProductAPI");
            Helper.ShoppingCartBaseApiUrl = serviceUrls.GetValue<string>("CartAPI");
    

            builder.Services.AddAutoMapper(typeof(MappingConfigs).Assembly);

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.ExpireTimeSpan = TimeSpan.FromHours(10);
                    options.LoginPath = "/Auth/Login";
                    options.AccessDeniedPath = "/Auth/AccessDenied";
                });

            return builder;
        }
    }
}
