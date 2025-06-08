using OnlineRestaurant.MVC.Models.Product;

namespace OnlineRestaurant.MVC.Models.ShoppingCart
{
    public class CartDetailDto
    {
        public int Id { get; set; }
      
        public int CartHeaderId { get; set; }
        public CartHeaderDto? CartHeader { get; set; }
        
        public int ProductId { get; set; }
        public ProductDto? Product { get; set; }
        
        public int Count { get; set; }
    }
}
