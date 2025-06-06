using OnlineRestaurant.MVC.Models;
using OnlineRestaurant.MVC.Models.Coupon;
using OnlineRestaurant.MVC.Service.IService;
using OnlineRestaurant.MVC.Utils;

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
                ApiType = ApiTypeEnum.GET,
                Url = $"{Helper.CouponBaseApiUrl}"
            });

            return response;
        }
        public async Task<ResponseDto> GetCouponByCodeAsync(string couponCode)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = ApiTypeEnum.GET,
                Url = $"{Helper.CouponBaseApiUrl}/GetByCode/{couponCode}"
            });
        }

        public async Task<ResponseDto> GetCouponByIdAsync(int couponId)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = ApiTypeEnum.GET,
                Url = $"{Helper.CouponBaseApiUrl}/{couponId}"
            });
        }


        public async Task<ResponseDto> CreateCouponAsync(CouponDto couponDto)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = ApiTypeEnum.POST,
                Url = $"{Helper.CouponBaseApiUrl}",
                Data = couponDto
            });
        }
        public async Task<ResponseDto> UpdateCouponAsync(CouponDto couponDto)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = ApiTypeEnum.PUT,
                Url = $"{Helper.CouponBaseApiUrl}/{couponDto.Id}",
                Data = couponDto
            });
        }

        public async Task<ResponseDto> DeleteCouponAsync(int couponId)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = ApiTypeEnum.DELETE,
                Url = $"{Helper.CouponBaseApiUrl}/{couponId}"
            });
        }
    }
}
