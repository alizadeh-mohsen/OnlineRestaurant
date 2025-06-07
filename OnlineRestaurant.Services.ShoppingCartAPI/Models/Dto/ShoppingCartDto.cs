namespace OnlineRestaurant.Services.ShoppingCartAPI.Models.Dto
{
    public class ShoppingCartDto
    {
        public CartHeaderDto? CartHeader { get; set; }
        public List<CartDetailDto>? CartDetails { get; set; } 
    }
}
