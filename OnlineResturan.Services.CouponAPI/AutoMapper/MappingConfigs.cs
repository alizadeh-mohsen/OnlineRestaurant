using AutoMapper;
using OnlineRestaurant.Services.CouponAPI.Models;
using OnlineRestaurant.Services.CouponAPI.Models.Dto;

namespace OnlineRestaurant.Services.CouponAPI.AutoMapper
{
    public class MappingConfigs : Profile
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfigs = new MapperConfiguration(config =>
            {
                config.CreateMap<Coupon, CouponDto>();
                config.CreateMap<CouponDto, Coupon>();
            });
            return mappingConfigs;
        }
    }
}
