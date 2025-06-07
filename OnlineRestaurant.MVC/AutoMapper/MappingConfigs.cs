using AutoMapper;
using OnlineRestaurant.MVC.Models.Coupon;
using OnlineRestaurant.MVC.Models.Dto;

namespace OnlineRestaurant.MVC.AutoMapper
{
    public class MappingConfigs : Profile
    {
        public MappingConfigs()
        {
            CreateMap<CouponViewModel, CouponDto>().ReverseMap();
        }
    }
}
