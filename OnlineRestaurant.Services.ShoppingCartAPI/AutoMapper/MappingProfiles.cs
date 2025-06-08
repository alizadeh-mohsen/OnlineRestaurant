using AutoMapper;
using OnlineRestaurant.Services.ShoppingCartAPI.Models;
using OnlineRestaurant.Services.ShoppingCartAPI.Models.Dto;

namespace OnlineRestaurant.Services.ShoppingCartAPI.AutoMapper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<CartHeader, CartHeaderDto>().ReverseMap();
            CreateMap<CartDetail, CartDetailDto>().ReverseMap();
        }
    }
}

