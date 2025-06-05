using OnlineRestaurant.MVC.Utils;

namespace OnlineRestaurant.MVC.Service.IService
{
    public class TokenProvider : ITokenProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TokenProvider(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void ClearToken()
        {
            _httpContextAccessor?.HttpContext?.Response.Cookies.Delete(Keys.TokenCookieName);
        }

        public string? GetToken()
        {
            string? token = null;
            _httpContextAccessor?.HttpContext?.Request.Cookies.TryGetValue(Keys.TokenCookieName, out token);
            return token;
        }

        public void SetToken(string token)
        {
            _httpContextAccessor?.HttpContext?.Response.Cookies.Append(Keys.TokenCookieName, token);
        }
    }
}
