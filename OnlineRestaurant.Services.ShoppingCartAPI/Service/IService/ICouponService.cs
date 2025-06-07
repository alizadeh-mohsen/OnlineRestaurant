using OnlineRestaurant.Services.ShoppingCartAPI.Models.Dto;

namespace OnlineRestaurant.Services.ShoppingCartAPI.Service.IService
{
    public interface ICouponService
    {
        Task<ResponseDto> GetCoupon(string couponCode);
    }
}
