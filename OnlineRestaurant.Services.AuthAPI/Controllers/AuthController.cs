using Microsoft.AspNetCore.Mvc;
using OnlineRestaurant.Services.AuthAPI.Models.Dto;
using OnlineRestaurant.Services.AuthAPI.Services.IService;

namespace OnlineRestaurant.Services.AuthAPI.Controllers
{
    [ApiController]
    [Route("api/Auth")]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        protected ResponseDto _responseDto;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
            _responseDto = new ResponseDto();
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var loginResponseDto = await _authService.Login(loginRequestDto);

            if (loginResponseDto != null && !string.IsNullOrEmpty(loginResponseDto.Token))
            {
                _responseDto.Result = loginResponseDto;
                return Ok(_responseDto);
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
