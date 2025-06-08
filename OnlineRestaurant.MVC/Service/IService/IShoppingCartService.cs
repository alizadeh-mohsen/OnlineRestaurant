using OnlineRestaurant.MVC.Models;
using OnlineRestaurant.MVC.Models.ShoppingCart;

namespace OnlineRestaurant.MVC.Service.IService
{
    public interface IShoppingCartService
    {
        Task<ResponseDto> UpSert(ShoppingCartDto requestDto);
        Task<ResponseDto> GetCartByUserId(string userId);
        Task<ResponseDto> RemoveItem(int detailId);
        Task<ResponseDto> ClearCart(string userId);
        Task<ResponseDto> ApplyCoupon(ShoppingCartDto shoppingCartDto);

    }
}
