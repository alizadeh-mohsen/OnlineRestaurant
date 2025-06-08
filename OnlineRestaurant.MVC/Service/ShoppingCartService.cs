using OnlineRestaurant.MVC.Models;
using OnlineRestaurant.MVC.Models.ShoppingCart;
using OnlineRestaurant.MVC.Service.IService;
using OnlineRestaurant.MVC.Utils;

namespace OnlineRestaurant.MVC.Service
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IBaseService _baseService;

        public ShoppingCartService(IBaseService baseService)
        {
            _baseService = baseService;
        }
        public async Task<ResponseDto> ApplyCoupon(ShoppingCartDto shoppingCartDto)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = ApiTypeEnum.POST,
                Url = $"{Helper.ShoppingCartBaseApiUrl}/ApplyCoupon/",
                Data = shoppingCartDto
            });
        }

        public async Task<ResponseDto> ClearCart(string userId)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = ApiTypeEnum.POST,
                Url = $"{Helper.ShoppingCartBaseApiUrl}/ClearCart/{userId}"
            });
        }

        public async Task<ResponseDto> GetCartByUserId(string userId)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = ApiTypeEnum.GET,
                Url = $"{Helper.ShoppingCartBaseApiUrl}/{userId}"
            });
        }

        public async Task<ResponseDto> RemoveItem(int detailId)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = ApiTypeEnum.POST,
                Url = $"{Helper.ShoppingCartBaseApiUrl}/RemoveItem/",
                Data = detailId
            });
        }

        public async Task<ResponseDto> UpSert(ShoppingCartDto shoppingCartDto)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = ApiTypeEnum.POST,
                Url = $"{Helper.ShoppingCartBaseApiUrl}/Upsert/",
                Data = shoppingCartDto
            });
        }
    }
}
