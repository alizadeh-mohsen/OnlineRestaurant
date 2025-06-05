using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OnlineRestaurant.MVC.Models.Auth;
using OnlineRestaurant.MVC.Service.IService;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace OnlineRestaurant.MVC.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly ITokenProvider _tokenProvider;

        public AuthController(IAuthService authService, ITokenProvider tokenProvider)
        {
            _authService = authService;
            _tokenProvider = tokenProvider;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            RegisterRequestDto model = new RegisterRequestDto();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterRequestDto registerDto)
        {
            if (ModelState.IsValid)
            {
                var response = await _authService.RegisterAsync(registerDto);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction("Login", "Auth");
                }

                TempData["error"] = response?.Message ?? "Registration failed. Please try again.";
            }
            return View(registerDto);
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            LoginRequestDto model = new LoginRequestDto();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestDto loginDto)
        {
            if (ModelState.IsValid)
            {
                var response = await _authService.LoginAsync(loginDto);
                if (response != null && response.IsSuccess)
                {
                    LoginResponseDto responseDto = JsonConvert.DeserializeObject<LoginResponseDto>(Convert.ToString(response.Result));
                    await SignInUser(responseDto);
                    _tokenProvider.SetToken(responseDto.Token);

                    return RedirectToAction("Index", "Home");
                }
                TempData["error"] = response?.Message ?? "Login failed. Please try again.";
            }
            return View(loginDto);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            _tokenProvider.ClearToken();
            TempData["success"] = "Logged out successfully!";
            return RedirectToAction("Index", "Home");
        }


        private async Task SignInUser(LoginResponseDto? responseDto)
        {
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(responseDto?.Token ?? string.Empty);

            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Email, token.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email).Value));
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Sub, token.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Sub).Value));
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Name, token.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Name).Value));

            identity.AddClaim(new Claim(ClaimTypes.Name, token.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Name).Value));
            identity.AddClaim(new Claim(ClaimTypes.Role, token.Claims.FirstOrDefault(u => u.Type == "role").Value));

            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

        }
    }
}



