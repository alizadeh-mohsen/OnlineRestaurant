using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OnlineRestaurant.MVC.Models.Product;
using OnlineRestaurant.MVC.Service.IService;

namespace OnlineRestaurant.MVC.Controllers
{
    public class ProductController : Controller

    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        public async Task<IActionResult> Index()
        {
            var model = new List<ProductDto>();
            var responseDto = await _productService.GetProductsAsync();
            if (responseDto.IsSuccess)
                model = JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(responseDto.Result));
            else
                TempData["error"] = responseDto.Message;

            return View(model);
        }

        public async Task<IActionResult> Details(int id)
        {
            var model = new ProductDto();

            var responseDto = await _productService.GetProductAsync(id);
            if (responseDto.IsSuccess)
                model = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(responseDto.Result));
            else
                TempData["error"] = responseDto.Message;
            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductDto productDto)
        {
            if (ModelState.IsValid)
            {
                var responseDto = await _productService.CreateProductAsync(productDto);
                if (responseDto.IsSuccess)
                {
                    TempData["success"] = "Product created successfully.";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["error"] = responseDto.Message;
                }
            }
            return View(productDto);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var model = await GetProductById(id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductDto productDto)
        {
            var model = new ProductDto();
            var responseDto = await _productService.UpdateProductAsync(productDto);
            if (responseDto.IsSuccess)
                return RedirectToAction(nameof(Index));
            else
            {
                TempData["error"] = responseDto.Message;
                return View(model);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            var model = await GetProductById(id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id, IFormCollection collection)
        {
            var responseDto = await _productService.DeleteProductAsync(id);
            if (responseDto.IsSuccess)
                return RedirectToAction(nameof(Index));
            else
            {
                TempData["error"] = responseDto.Message;
                return View();
            }
        }


        private async Task<ProductDto> GetProductById(int id)
        {
            var response = await _productService.GetProductAsync(id);
            if (!response.IsSuccess)
            {
                TempData["error"] = response.Message;
                return null; // Return an empty CouponDto if not found
            }
            return JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
        }

    }
}
