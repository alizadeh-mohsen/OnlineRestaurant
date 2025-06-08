using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OnlineRestaurant.MVC.Models.ShoppingCart;
using OnlineRestaurant.MVC.Service.IService;
using System.IdentityModel.Tokens.Jwt;

namespace OnlineRestaurant.MVC.Controllers
{
    public class CartController : Controller
    {
        private readonly IShoppingCartService _cartService;

        public CartController(IShoppingCartService cartService)
        {
            _cartService = cartService;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)?.Value;
            var response = await _cartService.GetCartByUserId(userId);
            if (response.IsSuccess)
            {
                var shoppingCart = JsonConvert.DeserializeObject<ShoppingCartDto>(Convert.ToString(response.Result));
                return View(shoppingCart);
            }
            else
            {
                TempData["error"] = response.Message;
                return View(new ShoppingCartDto());
            }

        }

        [HttpPost]
        public async Task<IActionResult> ApplyCoupon(ShoppingCartDto shoppingCartDto)
        {
            var response = await _cartService.ApplyCoupon(shoppingCartDto);
            if (response.IsSuccess)
                TempData["success"] = "Coupon applied successfully";
            else
                TempData["error"] = response.Message;

            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Remove(int detailId)
        {
            var responseDto = await _cartService.RemoveItem(detailId);

            if (responseDto.IsSuccess)
                TempData["success"] = "Item deleted successfully";
            else
                TempData["error"] = responseDto.Message;
            
            return RedirectToAction(nameof(Index));
        }
    }
}
