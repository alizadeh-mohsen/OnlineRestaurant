using OnlineRestaurant.Services.AuthAPI.Models.Dto;

namespace OnlineRestaurant.MVC.Models.Auth
{
    public class LoginResponseDto
    {
        public string Token { get; set; }
        public UserDto UserDto { get; set; }
    }
}
