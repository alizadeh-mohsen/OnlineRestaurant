namespace OnlineRestaurant.Services.ProductAPI.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; } = "https://picsum.photos/seed/picsum/200/300";
        public int Discount { get; set; }
        public string? CategoryName { get; set; }
    }
}
