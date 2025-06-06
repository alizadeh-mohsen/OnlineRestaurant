using System.ComponentModel.DataAnnotations;

namespace OnlineRestaurant.MVC.Models.Product
{
    public class ProductDto
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        public string? Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string? ImageUrl { get; set; }
        [Required]
        public int Discount { get; set; }
        public string? CategoryName { get; set; }
        [Display(Name = "Count")]
        public int OrderCount { get; set; }
    }
}
