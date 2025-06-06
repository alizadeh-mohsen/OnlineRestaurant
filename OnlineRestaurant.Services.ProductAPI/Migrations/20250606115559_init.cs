using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OnlineRestaurant.Services.ProductAPI.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Discount = table.Column<int>(type: "int", nullable: false),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryName", "Description", "Discount", "ImageUrl", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "Fast Food", "Delicious cheese pizza", 0, "https://fastly.picsum.photos/id/64/4326/2884.jpg?hmac=9_SzX666YRpR_fOyYStXpfSiJ_edO3ghlSRnH2w09Kg", "Pizza", 9.99m },
                    { 2, "Fast Food", "Juicy beef burger with lettuce and tomato", 0, "https://fastly.picsum.photos/id/65/4912/3264.jpg?hmac=uq0IxYtPIqRKinGruj45KcPPzxDjQvErcxyS1tn7bG0", "Burger", 5.99m },
                    { 3, "Italian", "Creamy Alfredo pasta with chicken", 0, "https://fastly.picsum.photos/id/325/4928/3264.jpg?hmac=D_X6AKqCcH8IpWElX5X3dxx11yn7yYO-vPhiKhzRbwI", "Pasta", 12.99m },
                    { 4, "Healthy", "Fresh garden salad with vinaigrette dressing", 0, "https://fastly.picsum.photos/id/230/1500/1500.jpg?hmac=heg53PqHqX88fhXrDyqlqJK8lLJXGRudsOXMKB3BZtc", "Salad", 7.99m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
