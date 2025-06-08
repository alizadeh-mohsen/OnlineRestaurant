using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OnlineRestaurant.MVC.Models;
using OnlineRestaurant.MVC.Models.Product;
using OnlineRestaurant.MVC.Models.ShoppingCart;
using OnlineRestaurant.MVC.Service.IService;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;

namespace OnlineRestaurant.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService _productService;
        private readonly IShoppingCartService _shoppingCartService;

        public HomeController(IProductService productService, IShoppingCartService shoppingCartService)
        {
            _productService = productService;
            _shoppingCartService = shoppingCartService;
        }

        public async Task<IActionResult> Index()
        {

            var responseDto = await _productService.GetProductsAsync();
            if (responseDto.IsSuccess)
            {
                var model = JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(responseDto.Result));
                return View(model);
            }
            else
            {
                TempData["error"] = responseDto.Message;
            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Details(int id)
        {
            var response = await _productService.GetProductAsync(id);
            if (response.IsSuccess)
            {
                var model = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
                model.OrderCount = 1;
                return View(model);
            }
            else
            {
                TempData["error"] = response.Message;
                return View();
            }
        }

        [Authorize]
        [HttpPost]
        [ActionName("Details")]
        public async Task<IActionResult> Details(ProductDto productDto)
        {
            if (productDto.OrderCount == 0)
            {
                TempData["error"] = "Select count";
                return RedirectToAction(nameof(Details));
            }
            var cartDto = new ShoppingCartDto
            {
                CartHeader = new CartHeaderDto
                {
                    UserId = User.Claims.FirstOrDefault(cartDto => cartDto.Type == JwtRegisteredClaimNames.Sub)?.Value,
                },
                CartDetails = new List<CartDetailDto>()
            };

            cartDto.CartDetails.Add(new CartDetailDto
            {
                ProductId = productDto.Id,
                Count = productDto.OrderCount,
            });

            var response = await _shoppingCartService.UpSert(cartDto);
            if (response.IsSuccess)
            {
                TempData["success"] = "Product added to cart successfully.";
            }
            else
            {
                TempData["error"] = response.Message;
            }
            return RedirectToAction(nameof(Index));
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
