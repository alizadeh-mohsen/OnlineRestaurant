using System.ComponentModel.DataAnnotations;

namespace OnlineRestaurant.MVC.Models.Auth
{
    public class LoginRequestDto
    {
        [Required]
        public string? Username { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}
