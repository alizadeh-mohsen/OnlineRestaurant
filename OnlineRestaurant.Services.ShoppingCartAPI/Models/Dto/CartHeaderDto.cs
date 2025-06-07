using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineRestaurant.Services.ShoppingCartAPI.Models.Dto
{
    public class CartHeaderDto
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public string? CouponCode { get; set; }
        public decimal Discount { get; set; }
        public decimal CartTotal { get; set; }
    }
}
