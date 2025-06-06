using System.ComponentModel.DataAnnotations;

namespace OnlineRestaurant.MVC.Models.Coupon
{
    public class CouponDto
    {
        public int Id { get; set; }
        [Required]
        public string? CouponCode { get; set; }
        [Required]
        public double DiscountAmount { get; set; }
        [Required]
        public double MinAmount { get; set; }
    }
}
