using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineRestaurant.Services.ShoppingCartAPI.Models
{
    public class CartHeader
    {
        public int Id { get; set; }

        public string? UserId { get; set; }
        public string? CouponCode { get; set; }

        [NotMapped]
        public decimal Discount { get; set; }
        [NotMapped]
        public decimal CartTotal { get; set; }
    }
}
