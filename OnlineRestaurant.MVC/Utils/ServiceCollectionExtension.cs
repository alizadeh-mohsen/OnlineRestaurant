using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using OnlineRestaurant.MVC.AutoMapper;
using OnlineRestaurant.MVC.Service;
using OnlineRestaurant.MVC.Service.IService;

namespace OnlineRestaurant.MVC.Utils
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services, WebApplicationBuilder builder)
        {
            services.AddHttpContextAccessor();
            services.AddHttpClient();
            services.AddHttpClient<ICouponService, CouponService>();
            services.AddHttpClient<IAuthService, AuthService>();

            Helper.CouponBaseApi = builder.Configuration["ServiceUrls:CouponAPI"];
            Helper.AuthBaseApi = builder.Configuration["ServiceUrls:AuthAPI"];

            services.AddScoped<IBaseService, BaseService>();
            services.AddScoped<ICouponService, CouponService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ITokenProvider, TokenProvider>();

            IMapper mapper = MappingConfigs.RegisterMaps().CreateMapper();
            services.AddSingleton(mapper);
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.ExpireTimeSpan = TimeSpan.FromHours(10);
                    options.LoginPath = "/Auth/Login";
                    options.AccessDeniedPath = "/Auth/AccessDenied";
                });

            return services;
        }
    }
}
