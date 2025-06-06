using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OnlineRestaurant.MVC.Models;
using OnlineRestaurant.MVC.Models.Product;
using OnlineRestaurant.MVC.Service.IService;
using System.Diagnostics;

namespace OnlineRestaurant.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService _productService;

        public HomeController(IProductService productService)
        {
            _productService = productService;
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
                return View(model);
            }
            else
            {
                TempData["error"] = response.Message;
                return View();
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
