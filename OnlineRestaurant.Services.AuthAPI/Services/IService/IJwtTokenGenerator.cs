using OnlineRestaurant.Services.AuthAPI.Models;

namespace OnlineRestaurant.Services.AuthAPI.Services.IService
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(ApplicationUser applicationUser, string role);

    }
}
