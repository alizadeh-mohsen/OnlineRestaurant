using Microsoft.AspNetCore.Mvc;
using OnlineRestaurant.Services.AuthAPI.Models.Dto;
using OnlineRestaurant.Services.AuthAPI.Services.IService;

namespace OnlineRestaurant.Services.AuthAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {

        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var response = await _authService.Login(loginRequestDto);

            if (response != null && !string.IsNullOrEmpty(response.Token))
            {
                return Ok(response);
            }

            return BadRequest(new ResponseDto
            {
                IsSuccess = false,
                Message = "Login failed!",
                Result = null
            });
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {
            var response = await _authService.Register(registerRequestDto);
            if (response.IsSuccess)
            {
                return Ok(new ResponseDto
                {
                    IsSuccess = true,
                    Message = "Registration successful!",
                    Result = null
                });
            }
            return BadRequest(new ResponseDto
            {
                IsSuccess = false,
                Message = response.Message,
                Result = null
            });
        }
    }
}
