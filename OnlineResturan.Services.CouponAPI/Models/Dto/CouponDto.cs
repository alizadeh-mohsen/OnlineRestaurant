﻿namespace OnlineRestaurant.Services.CouponAPI.Models.Dto
{
    public class CouponDto
    {
        public int Id { get; set; }
        public string? CouponCode { get; set; }
        public decimal Discount { get; set; }
        public decimal MinAmount { get; set; }
    }
}
