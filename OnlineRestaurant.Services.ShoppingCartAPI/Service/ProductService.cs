using Newtonsoft.Json;
using OnlineRestaurant.Services.ShoppingCartAPI.Models;
using OnlineRestaurant.Services.ShoppingCartAPI.Models.Dto;
using OnlineRestaurant.Services.ShoppingCartAPI.Service.IService;
using OnlineRestaurant.Services.ShoppingCartAPI.Utils;

namespace OnlineRestaurant.Services.ShoppingCartAPI.Service
{
    public class ProductService : IProductService
    {
        private readonly IBaseService _baseService;

        public ProductService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<IEnumerable<ProductDto>> GetProductListAsync(IEnumerable<int> productIds)
        {
            var responseDto = await _baseService.SendAsync(new RequestDto
            {
                ApiType = ApiTypeEnum.GET,
                Url = $"{Helper.ProductBaseApiUrl}/GetListOfProductsByIds",
                Data = productIds
            });

            if (responseDto.IsSuccess)
            {
                var result = JsonConvert.DeserializeObject<IEnumerable<ProductDto>>(Convert.ToString(responseDto.Result));
                return result;
            }

            return null;

        }
    }
}
