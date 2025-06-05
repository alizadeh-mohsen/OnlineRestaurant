using AutoMapper;
using OnlineRestaurant.MVC.Models.Coupon;
using OnlineRestaurant.MVC.Models.Dto;

namespace OnlineRestaurant.MVC.AutoMapper
{
    public class MappingConfigs : Profile
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfigs = new MapperConfiguration(config =>
            {
                config.CreateMap<CouponViewModel, CouponDto>().ReverseMap();
            });
            return mappingConfigs;
        }
    }
}
