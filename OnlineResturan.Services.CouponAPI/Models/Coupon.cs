namespace OnlineRestaurant.Services.CouponAPI.Models
{
    public class Coupon
    {
        public int Id { get; set; }
        public string? CouponCode { get; set; }
        public double DiscountAmount { get; set; }
        public double MinAmount { get; set; }
        //public DateTime CreatedDate { get; set; }
        //public DateTime ExpiryDate { get; set; }
        //public bool IsActive { get; set; }
        //public string? CreatedBy { get; set; }
        //public string? UpdatedBy { get; set; }
        //public DateTime UpdatedDate { get; set; }
    }
}
