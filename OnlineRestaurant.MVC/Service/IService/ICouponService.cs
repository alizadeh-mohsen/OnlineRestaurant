using OnlineRestaurant.MVC.Models;
using OnlineRestaurant.MVC.Models.Coupon;

namespace OnlineRestaurant.MVC.Service.IService
{
    public interface ICouponService
    {
        Task<ResponseDto> GetCouponByCodeAsync(string couponCode);
        Task<ResponseDto> GetCouponByIdAsync(int couponId);
        Task<ResponseDto> GetAllCouponsAsync();
        Task<ResponseDto> CreateCouponAsync(CouponDto couponDto);
        Task<ResponseDto> UpdateCouponAsync(CouponDto couponDto);
        Task<ResponseDto> DeleteCouponAsync(int couponId);
    }
}
