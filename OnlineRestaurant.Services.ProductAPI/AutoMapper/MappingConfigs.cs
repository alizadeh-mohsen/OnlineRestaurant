using AutoMapper;
using OnlineRestaurant.Services.ProductAPI.Models;
using OnlineRestaurant.Services.ProductAPI.Models.Dto;

namespace OnlineRestaurant.Services.ProductAPI.AutoMapper
{
    public class MappingConfigs : Profile
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfigs = new MapperConfiguration(config =>
            {
                config.CreateMap<Product, ProductDto>();
            });
            return mappingConfigs;
        }
    }
}
