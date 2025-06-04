namespace OnlineRestaurant.Services.AuthAPI.Models.Dto
{
    public class LoginResponseDto
    {
        public string Token { get; set; }
        public UserDto UserDto { get; set; }
    }
}
