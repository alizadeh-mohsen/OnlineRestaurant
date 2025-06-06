using OnlineRestaurant.MVC.Models;
using OnlineRestaurant.MVC.Models.Product;
using OnlineRestaurant.MVC.Service.IService;
using OnlineRestaurant.MVC.Utils;

namespace OnlineRestaurant.MVC.Service
{
    public class ProductService : IProductService
    {
        private readonly IBaseService _baseService;

        public ProductService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDto> GetProductsAsync()
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = ApiTypeEnum.GET,
                Url = $"{Helper.ProductBaseApiUrl}"
            });
        }

        public async Task<ResponseDto> GetProductAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = ApiTypeEnum.GET,
                Url = $"{Helper.ProductBaseApiUrl}/{id}"
            });
        }

        public async Task<ResponseDto> CreateProductAsync(ProductDto productDto)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = ApiTypeEnum.POST,
                Url = $"{Helper.ProductBaseApiUrl}",
                Data = productDto
            });
        }

        public async Task<ResponseDto> DeleteProductAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = ApiTypeEnum.DELETE,
                Url = $"{Helper.ProductBaseApiUrl}/{id}"
            });
        }
        public async Task<ResponseDto> UpdateProductAsync(ProductDto productDto)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = ApiTypeEnum.PUT,
                Url = $"{Helper.ProductBaseApiUrl}/{productDto.Id}",
                Data = productDto
            });
        }
    }
}
