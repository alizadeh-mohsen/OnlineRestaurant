using OnlineRestaurant.MVC.Models;
using OnlineRestaurant.MVC.Models.Auth;

namespace OnlineRestaurant.MVC.Service.IService
{
    public interface IAuthService
    {
        Task<ResponseDto> LoginAsync(LoginRequestDto loginDto);
        Task<ResponseDto> RegisterAsync(RegisterRequestDto registerDto);
    }
}
