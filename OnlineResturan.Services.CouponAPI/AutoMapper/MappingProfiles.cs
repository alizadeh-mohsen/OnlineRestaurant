using AutoMapper;
using OnlineRestaurant.Services.CouponAPI.Models;
using OnlineRestaurant.Services.CouponAPI.Models.Dto;

namespace OnlineRestaurant.Services.CouponAPI.AutoMapper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Coupon, CouponDto>().ReverseMap();
        }
    }
}
