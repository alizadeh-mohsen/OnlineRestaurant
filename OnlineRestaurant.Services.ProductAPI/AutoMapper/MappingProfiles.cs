using AutoMapper;
using OnlineRestaurant.Services.ProductAPI.Models;
using OnlineRestaurant.Services.ProductAPI.Models.Dto;

namespace OnlineRestaurant.Services.ProductAPI.AutoMapper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
        }
    }
}
