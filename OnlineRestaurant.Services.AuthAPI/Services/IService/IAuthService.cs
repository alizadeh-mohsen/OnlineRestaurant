using OnlineRestaurant.Services.AuthAPI.Models.Dto;

namespace OnlineRestaurant.Services.AuthAPI.Services.IService
{
    public interface IAuthService
    {
        Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
        Task<RegisterResponseDto> Register(RegisterRequestDto registerRequestDto);
    }
}
