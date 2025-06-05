using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OnlineRestaurant.MVC.Models.Coupon;
using OnlineRestaurant.MVC.Service.IService;

namespace OnlineRestaurant.MVC.Controllers
{
    public class CouponController : Controller
    {
        private readonly ICouponService _couponService;

        public CouponController(ICouponService couponService)
        {
            _couponService = couponService;
        }

        public async Task<IActionResult> Index()
        {
            List<CouponDto>? coupons = new List<CouponDto>();

            var response = await _couponService.GetAllCouponsAsync();

            if (response.IsSuccess)
            {
                TempData["success"] = response.Message;
                coupons = JsonConvert.DeserializeObject<List<CouponDto>>(Convert.ToString(response.Result));
            }
            else
            {
                TempData["error"] = response.Message;
            }

            return View(coupons);
        }


        public async Task<ActionResult> Details(int id)
        {
            var coupon = await GetCouponById(id);
            return View(coupon);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CouponDto couponDto)
        {
            var response = await _couponService.CreateCouponAsync(couponDto);
            if (response.IsSuccess)
            {
                TempData["success"] = "The villa has been created successfully";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["error"] = response.Message;
                ModelState.AddModelError(string.Empty, response.Message);
            }

            return View(couponDto);
        }


        public async Task<ActionResult> Edit(int id)
        {
            var coupon = await GetCouponById(id);
            return View(coupon);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(CouponDto couponDto)
        {
            try
            {
                var response = await _couponService.UpdateCouponAsync(couponDto);
                TempData["success"] = response.IsSuccess ? 
                    "Coupon updated successfully." :
                    response.Message;
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return View();
            }
        }


        public async Task<ActionResult> Delete(int id)
        {
            var coupon = await GetCouponById(id);
            return View(coupon);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                var respone = await _couponService.DeleteCouponAsync(id);
                if (respone.IsSuccess)
                {
                    TempData["success"] = "Coupon deleted successfully.";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["error"] = respone.Message;
                    return View();
                }

            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return View();
            }
        }

        private async Task<CouponDto> GetCouponById(int id)
        {
            var response = await _couponService.GetCouponByIdAsync(id);
            if (!response.IsSuccess)
            {
                TempData["error"] = response.Message;
                return null; // Return an empty CouponDto if not found
            }
            return JsonConvert.DeserializeObject<CouponDto>(Convert.ToString(response.Result));
        }
    }
}
