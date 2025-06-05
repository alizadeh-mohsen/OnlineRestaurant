using Newtonsoft.Json;
using OnlineRestaurant.MVC.Models;
using OnlineRestaurant.MVC.Models.Auth;
using OnlineRestaurant.MVC.Service.IService;
using OnlineRestaurant.MVC.Utils;

namespace OnlineRestaurant.MVC.Service
{
    public class AuthService : IAuthService
    {
        private readonly IBaseService _baserService;
        public AuthService(IBaseService baseService)
        {
            _baserService = baseService;

        }

        public IBaseService BaseService { get; }

        public async Task<ResponseDto> LoginAsync(LoginRequestDto loginDto)
        {
            return await _baserService.SendAsync(new RequestDto
            {
                ApiType = ApiTypeEnum.POST,
                Url = $"{Helper.AuthBaseApi}/api/Auth/Login",
                Data = loginDto
            }, false);

        }

        public Task<ResponseDto> RegisterAsync(RegisterRequestDto registerDto)
        {
            return _baserService.SendAsync(new RequestDto
            {
                ApiType = ApiTypeEnum.POST,
                Url = $"{Helper.AuthBaseApi}/api/Auth/Register",
                Data = registerDto
            }, false);
        }
    }
}
