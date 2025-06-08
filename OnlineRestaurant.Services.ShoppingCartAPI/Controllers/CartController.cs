using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OnlineRestaurant.Services.ShoppingCartAPI.Data;
using OnlineRestaurant.Services.ShoppingCartAPI.Models;
using OnlineRestaurant.Services.ShoppingCartAPI.Models.Dto;
using OnlineRestaurant.Services.ShoppingCartAPI.Service.IService;
using System.Reflection.PortableExecutable;

namespace OnlineRestaurant.Services.ShoppingCartAPI.Controllers
{
    [ApiController]
    [Route("api/cart")]
    //[Authorize]
    public class CartController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IProductService _productService;
        private readonly ICouponService _couponService;

        public CartController(AppDbContext dbContext, IMapper mapper, IProductService productService, ICouponService couponService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _productService = productService;
            _couponService = couponService;
        }

        [HttpPost("Upsert")]
        public async Task<ResponseDto> Upsert([FromBody] ShoppingCartDto cartDto)
        {
            try
            {

                var existingCart = await _dbContext.CartHeaders.FirstOrDefaultAsync(c => c.UserId == cartDto.CartHeader.UserId);
                if (existingCart == null)
                {
                    var newHeader = _mapper.Map<CartHeader>(cartDto.CartHeader);

                    await _dbContext.CartHeaders.AddAsync(newHeader);
                    await _dbContext.SaveChangesAsync();

                    var newDetails = _mapper.Map<List<CartDetail>>(cartDto.CartDetails);
                    newDetails.ForEach(detail => detail.CartHeaderId = newHeader.Id);

                    _dbContext.CartDetails.AddRange(newDetails);
                    _dbContext.SaveChanges();

                    return new ResponseDto
                    {
                        Result = new ShoppingCartDto
                        {
                            CartHeader = _mapper.Map<CartHeaderDto>(newHeader),
                            CartDetails = _mapper.Map<List<CartDetailDto>>(newDetails)
                        },
                        IsSuccess = true
                    };

                }
                else
                {
                    var detailDto = _mapper.Map<CartDetail>(cartDto.CartDetails.FirstOrDefault());

                    var dbCartDetail = await _dbContext.CartDetails.FirstOrDefaultAsync(c => c.CartHeaderId == existingCart.Id && c.ProductId == detailDto.ProductId);
                    if (dbCartDetail == null)
                    {
                        detailDto.CartHeaderId = existingCart.Id;
                        _dbContext.CartDetails.Add(detailDto);
                    }

                    else
                    {
                        dbCartDetail.Count += detailDto.Count;
                        _dbContext.CartDetails.Update(dbCartDetail);
                    }
                    await _dbContext.SaveChangesAsync();

                    return new ResponseDto
                    {
                        Result = true,
                        IsSuccess = true
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseDto
                {
                    IsSuccess = false,
                    Message = ex.Message + " " + ex.InnerException?.Message
                };
            }
        }

        [HttpPost("RemoveItem")]
        public async Task<ResponseDto> RemoveItem([FromBody] int detailId)
        {
            try
            {
                var detail = await _dbContext.CartDetails.FirstOrDefaultAsync(c => c.Id == detailId);
                if (detail == null)
                {
                    return new ResponseDto
                    {
                        IsSuccess = false,
                        Message = "Cart item not found."
                    };
                }

                _dbContext.CartDetails.Remove(detail);
                await _dbContext.SaveChangesAsync();
                var details = await _dbContext.CartDetails.CountAsync(c => c.CartHeaderId == detail.CartHeaderId);
                if (details == 0)
                {
                    var header = await _dbContext.CartHeaders.FirstOrDefaultAsync(c => c.Id == detail.CartHeaderId);
                    if (header != null)
                    {
                        _dbContext.CartHeaders.Remove(header);
                        await _dbContext.SaveChangesAsync();
                    }
                }

                return new ResponseDto { IsSuccess = true, Message = "Item removed successfully." };
            }
            catch (Exception ex)
            {
                return new ResponseDto
                {
                    IsSuccess = false,
                    Message = ex.Message + " " + ex.InnerException?.Message
                };
            }
        }


        [HttpGet("{userId}")]
        public async Task<ResponseDto> GetShoppingCart(string userId)
        {
            try
            {
                var header = await _dbContext.CartHeaders.FirstOrDefaultAsync(h => h.UserId == userId);

                if (header != null)
                {
                    var details = await _dbContext.CartDetails.Where(x => x.CartHeaderId == header.Id).ToListAsync();

                    var productIds = details.Select(x => x.ProductId).ToList();
                    var products = await _productService.GetProductListAsync(productIds);

                    details.ForEach(d => d.Product = products.FirstOrDefault(p => p.Id == d.ProductId));

                    var totalPrice = details.Sum(d => d.Count * (d.Product?.Price ?? 0));
                    header.CartTotal = totalPrice;

                    if (!string.IsNullOrWhiteSpace(header.CouponCode))
                    {
                        var responseDto = await _couponService.GetCoupon(header.CouponCode);
                        if (responseDto.IsSuccess && responseDto.Result != null)
                        {
                            var coupon = JsonConvert.DeserializeObject<CouponDto>(Convert.ToString(responseDto.Result));
                            if (header.CartTotal > coupon.MinAmount)
                            {
                                header.CouponCode = coupon.CouponCode;
                                header.Discount = coupon.Discount;
                            }
                            else
                            {
                                header.CouponCode = null;
                                header.Discount = 0;
                            }
                            header.CartTotal -= coupon.Discount;
                        }
                        else {
                            return new ResponseDto
                            {
                                IsSuccess = false,
                                Result = new ShoppingCartDto()
                                {
                                    CartHeader = _mapper.Map<CartHeaderDto>(header),
                                    CartDetails = _mapper.Map<List<CartDetailDto>>(details)
                                },
                                Message = "Invaild Coupon code"
                            };
                        }
                    }


                    return new ResponseDto
                    {
                        IsSuccess = true,
                        Result = new ShoppingCartDto()
                        {
                            CartHeader = _mapper.Map<CartHeaderDto>(header),
                            CartDetails = _mapper.Map<List<CartDetailDto>>(details)
                        }
                    };
                }
                return new ResponseDto
                {
                    IsSuccess = false,
                    Message = "Shopping Cart is empty"
                };
            }
            catch (Exception ex)
            {

                return new ResponseDto
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }


        [HttpPost("ApplyCoupon")]
        public async Task<ResponseDto> ApplyCoupon([FromBody] ShoppingCartDto cartDto)
        {
            try
            {
                var header = await _dbContext.CartHeaders.FirstOrDefaultAsync(h => h.UserId == cartDto.CartHeader.UserId);
                if (header == null)
                {
                    return new ResponseDto { IsSuccess = false, Message = "Cart not found." };
                }
                header.CouponCode = cartDto.CartHeader.CouponCode;
                _dbContext.CartHeaders.Update(header);
                await _dbContext.SaveChangesAsync();
                return new ResponseDto { IsSuccess = true, Result = _mapper.Map<CartHeaderDto>(header) };
            }
            catch (Exception ex)
            {
                return new ResponseDto { IsSuccess = false, Message = ex.Message };
            }
        }

        [HttpPost("ClearCart")]
        public async Task<ResponseDto> ClearCart(string UserId)
        {
            try
            {
                var cartHeader = await _dbContext.CartHeaders.FirstOrDefaultAsync(c => c.UserId == UserId);
                _dbContext.CartDetails.RemoveRange(_dbContext.CartDetails.Where(c => c.CartHeaderId == cartHeader.Id));
                _dbContext.CartHeaders.Remove(cartHeader);

                await _dbContext.SaveChangesAsync();

                return new ResponseDto { IsSuccess = true, Message = "Cart deleted successfully." };
            }
            catch (Exception ex)
            {
                return new ResponseDto
                {
                    IsSuccess = false,
                    Message = ex.Message + " " + ex.InnerException?.Message
                };
            }
        }
    }
}