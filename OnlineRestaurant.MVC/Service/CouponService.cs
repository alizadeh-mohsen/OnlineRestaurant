using OnlineRestaurant.MVC.Models;
using OnlineRestaurant.MVC.Service.IService;

namespace OnlineRestaurant.MVC.Service
{
    public class CouponService : ICouponService
    {
        private readonly IBaseService _baseService;


        public CouponService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDto> GetAllCouponsAsync()
        {
            var response = await _baseService.SendAsync(new RequestDto
            {
                ApiType = Enums.ApiType.GET,
                Url = $"{Helpers.Helper.CouponAPIBase}/api/Coupon"
            });

            return response;
        }
        public async Task<ResponseDto> GetCouponByCodeAsync(string couponCode)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = Enums.ApiType.GET,
                Url = $"{Helpers.Helper.CouponAPIBase}/api/Coupon/GetByCode/{couponCode}"
            });
        }

        public async Task<ResponseDto> GetCouponByIdAsync(int couponId)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = Enums.ApiType.GET,
                Url = $"{Helpers.Helper.CouponAPIBase}/api/Coupon/{couponId}"
            });
        }


        public async Task<ResponseDto> CreateCouponAsync(CouponDto couponDto)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = Enums.ApiType.POST,
                Url = $"{Helpers.Helper.CouponAPIBase}/api/Coupon",
                Data = couponDto
            });
        }
        public async Task<ResponseDto> UpdateCouponAsync(CouponDto couponDto)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = Enums.ApiType.PUT,
                Url = $"{Helpers.Helper.CouponAPIBase}/api/Coupon/{couponDto.Id}",
                Data = couponDto
            });
        }

        public async Task<ResponseDto> DeleteCouponAsync(int couponId)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = Enums.ApiType.DELETE,
                Url = $"{Helpers.Helper.CouponAPIBase}/api/Coupon/{couponId}"
            });
        }
    }
}
