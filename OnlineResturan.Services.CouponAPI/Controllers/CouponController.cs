using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineRestaurant.Services.CouponAPI.Data;
using OnlineRestaurant.Services.CouponAPI.Models;
using OnlineRestaurant.Services.CouponAPI.Models.Dto;
using OnlineRestaurant.Services.CouponAPI.Utils;

namespace OnlineRestaurant.Services.CouponAPI.Controllers
{
    [ApiController]
    [Route("api/Coupon")]
    //[Authorize]
    public class CouponController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public CouponController(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseDto>> Get()
        {
            try
            {

                var coupons = await _dbContext.Coupons.ToListAsync();
                var responseDto = new ResponseDto
                {
                    Result = _mapper.Map<List<CouponDto>>(coupons),
                };

                return Ok(responseDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseDto
                {
                    IsSuccess = false,
                    Message = ex.Message
                });
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ResponseDto>> Get(int id)
        {
            try
            {
                var coupon = await _dbContext.Coupons.FindAsync(id);
                if (coupon == null)
                {
                    return NotFound(new ResponseDto
                    {
                        IsSuccess = false,
                        Message = "Coupon not found."
                    });
                }
                var responseDto = new ResponseDto
                {
                    Result = _mapper.Map<CouponDto>(coupon),
                };
                return Ok(responseDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseDto
                {
                    IsSuccess = false,
                    Message = ex.Message
                });
            }
        }

        [HttpGet("GetByCode/{code}")]
        public async Task<ActionResult<ResponseDto>> GetByCode(string code)
        {
            try
            {
                var coupon = await _dbContext.Coupons.FirstOrDefaultAsync(c => c.CouponCode == code);
                if (coupon == null)
                {
                    return NotFound(new ResponseDto
                    {
                        IsSuccess = false,
                        Message = "Coupon not found."
                    });
                }
                var responseDto = new ResponseDto
                {
                    Result = _mapper.Map<CouponDto>(coupon),
                };
                return Ok(responseDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseDto
                {
                    IsSuccess = false,
                    Message = ex.Message
                });
            }
        }

        [HttpPost]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult<ResponseDto>> Create([FromBody] CouponDto couponDto)
        {
            try
            {
                if (couponDto == null)
                {
                    return BadRequest(new ResponseDto
                    {
                        IsSuccess = false,
                        Message = "Invalid coupon data."
                    });
                }
                var coupon = _mapper.Map<Coupon>(couponDto);
                await _dbContext.Coupons.AddAsync(coupon);
                await _dbContext.SaveChangesAsync();
                var responseDto = new ResponseDto
                {
                    Result = _mapper.Map<CouponDto>(coupon),
                };
                return CreatedAtAction(nameof(Get), new { id = coupon.Id }, responseDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseDto
                {
                    IsSuccess = false,
                    Message = ex.Message
                });
            }
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult<ResponseDto>> Update(int id, [FromBody] CouponDto couponDto)
        {
            try
            {
                if (couponDto == null || id != couponDto.Id)
                {
                    return BadRequest(new ResponseDto
                    {
                        IsSuccess = false,
                        Message = "Invalid coupon data."
                    });
                }
                var existingCoupon = await _dbContext.Coupons.FindAsync(id);
                if (existingCoupon == null)
                {
                    return NotFound(new ResponseDto
                    {
                        IsSuccess = false,
                        Message = "Coupon not found."
                    });
                }
                var updatedCoupon = _mapper.Map(couponDto, existingCoupon);
                _dbContext.Coupons.Update(updatedCoupon);
                await _dbContext.SaveChangesAsync();
                var responseDto = new ResponseDto
                {
                    Result = _mapper.Map<CouponDto>(updatedCoupon),
                };
                return Ok(responseDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseDto
                {
                    IsSuccess = false,
                    Message = ex.Message
                });
            }
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult<ResponseDto>> Delete(int id)
        {
            try
            {
                var coupon = await _dbContext.Coupons.FindAsync(id);
                if (coupon == null)
                {
                    return NotFound(new ResponseDto
                    {
                        IsSuccess = false,
                        Message = "Coupon not found."
                    });
                }
                _dbContext.Coupons.Remove(coupon);
                await _dbContext.SaveChangesAsync();
                return Ok(new ResponseDto
                {
                    Result = "Coupon deleted successfully."
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseDto
                {
                    IsSuccess = false,
                    Message = ex.Message
                });
            }
        }
    }
}
