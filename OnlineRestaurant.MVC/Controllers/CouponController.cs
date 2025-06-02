using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OnlineRestaurant.MVC.Models;
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
                coupons = JsonConvert.DeserializeObject<List<CouponDto>>(Convert.ToString(response.Result));
            }
            else
            {
                ModelState.AddModelError(string.Empty, response.Message);
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
                TempData["success"] = "Coupon created successfully.";
                return RedirectToAction(nameof(Index));
            }
            else
            {
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
                return RedirectToAction(nameof(Index));
            }
            catch
            {
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
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                await _couponService.DeleteCouponAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        private async Task<CouponDto> GetCouponById(int id)
        {
            var response = await _couponService.GetCouponByIdAsync(id);
            return JsonConvert.DeserializeObject<CouponDto>(Convert.ToString(response.Result));

        }
    }
}
