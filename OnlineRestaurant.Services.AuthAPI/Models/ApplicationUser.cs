using Microsoft.AspNetCore.Identity;

namespace OnlineRestaurant.Services.AuthAPI.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? Name { get; set; }
    }
}
