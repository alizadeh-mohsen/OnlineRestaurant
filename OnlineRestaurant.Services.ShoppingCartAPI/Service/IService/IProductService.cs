using OnlineRestaurant.Services.ShoppingCartAPI.Models.Dto;

namespace OnlineRestaurant.Services.ShoppingCartAPI.Service.IService
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetProductListAsync(IEnumerable<int> productIds);
    }
}
