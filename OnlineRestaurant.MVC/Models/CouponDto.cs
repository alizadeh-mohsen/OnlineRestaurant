﻿namespace OnlineRestaurant.MVC.Models
{
    public class CouponDto
    {
        public int Id { get; set; }
        public string? CouponCode { get; set; }
        public double DiscountAmount { get; set; }
        public double MinAmount { get; set; }
    }
}
