namespace OnlineRestaurant.MVC.Models.Dto
{
    public class CouponViewModel
    {
        public int Id { get; set; }
        public string? CouponCode { get; set; }
        public double DiscountAmount { get; set; }
        public double MinAmount { get; set; }
    }
}
