using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineRestaurant.Services.AuthAPI.Data;
using OnlineRestaurant.Services.AuthAPI.Models;
using OnlineRestaurant.Services.AuthAPI.Models.Dto;
using OnlineRestaurant.Services.AuthAPI.Services.IService;

namespace OnlineRestaurant.Services.AuthAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AuthService(AppDbContext appDbContext, UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager, IJwtTokenGenerator jwtTokenGenerator)
        {
            _context = appDbContext;
            _userManager = userManager;
            _jwtTokenGenerator = jwtTokenGenerator;
        }
        public async Task<RegisterResponseDto> Register(RegisterRequestDto registerRequestDto)
        {
            try
            {
                ApplicationUser user = new ApplicationUser
                {
                    UserName = registerRequestDto.Username,
                    Email = registerRequestDto.Email,
                    NormalizedEmail = registerRequestDto.Email.ToUpper(),
                    EmailConfirmed = true,
                    Name=registerRequestDto.Name
                };

                var result = await _userManager.CreateAsync(user,registerRequestDto.Password);
                if (result.Succeeded)
                {
                    var registeredUser = _context.ApplicationUsers.FirstOrDefault(x => x.UserName == registerRequestDto.Username);
                    await _userManager.AddToRoleAsync(registeredUser, "customer");

                    return new RegisterResponseDto { IsSuccess = true, Message = "" };
                }
                return
                    new RegisterResponseDto
                    {
                        IsSuccess = false,
                        Message = result.Errors.FirstOrDefault()?.Description ?? "Registration failed due to an unknown error."
                    };
            }
            catch (Exception ex)
            {
                return
                    new RegisterResponseDto
                    {
                        IsSuccess = false,
                        Message = "An error occurred while registering the user: " + ex.Message
                    };
            }
        }

        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            var user = await _context.ApplicationUsers.FirstOrDefaultAsync(x => x.UserName.ToUpper() == loginRequestDto.Username.ToUpper());
            var isValid = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);
            if (isValid)
            {

                var role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
                var token = _jwtTokenGenerator.GenerateToken(user, role);

                return new LoginResponseDto
                {
                    Token = token,
                    UserDto = new UserDto
                    {
                        Email = user.Email,
                        UserName = user.UserName,
                        Id = user.Id,
                        Role = role
                    }
                };
            }
            return new LoginResponseDto
            {
                Token = "",
                UserDto = null
            };
        }


    }
}
