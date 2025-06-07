using OnlineRestaurant.Services.ShoppingCartAPI.Models;
using OnlineRestaurant.Services.ShoppingCartAPI.Models.Dto;
using OnlineRestaurant.Services.ShoppingCartAPI.Service.IService;
using OnlineRestaurant.Services.ShoppingCartAPI.Utils;

namespace OnlineRestaurant.Services.ShoppingCartAPI.Service
{
    public class CouponService : ICouponService
    {
        private readonly IBaseService _baseService;

        public CouponService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDto> GetCoupon(string couponCode)
        {
           return await _baseService.SendAsync(new RequestDto
            {
                ApiType = ApiTypeEnum.GET,
                Url = $"{Helper.CouponBaseApiUrl}/GetByCode/{couponCode}"
            }).ContinueWith(response =>
            {
                if (response.Result.IsSuccess)
                {
                    return response.Result;
                }
                else
                {
                    return new ResponseDto
                    {
                        IsSuccess = false,
                        Message = "Coupon not found."
                    };
                }
            });
        }
    }
}
