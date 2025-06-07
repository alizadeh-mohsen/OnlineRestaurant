namespace OnlineRestaurant.Services.ShoppingCartAPI.Utils
{
    public class Helper
    {
        public static string ProductBaseApiUrl { get; set; }
        public static string CouponBaseApiUrl { get; set; }
    }

    public enum ApiTypeEnum
    {
        GET,
        POST,
        PUT,
        DELETE
    }

}
