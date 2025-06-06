using OnlineRestaurant.MVC.Models;
using OnlineRestaurant.MVC.Models.Product;

namespace OnlineRestaurant.MVC.Service.IService
{
    public interface IProductService
    {
        Task<ResponseDto> GetProductsAsync();
        Task<ResponseDto> GetProductAsync(int id);
        Task<ResponseDto> CreateProductAsync(ProductDto productDto);
        Task<ResponseDto> UpdateProductAsync(ProductDto productDto);
        Task<ResponseDto> DeleteProductAsync(int id);

    }
}
