using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineRestaurant.Services.ProductAPI.Data;
using OnlineRestaurant.Services.ProductAPI.Models;
using OnlineRestaurant.Services.ProductAPI.Models.Dto;
using OnlineRestaurant.Services.ProductAPI.Utils;

namespace OnlineRestaurant.Services.ProductAPI.Controllers
{
    [ApiController]
    [Route("api/Product")]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _autoMapper;

        public ProductController(AppDbContext context, IMapper autoMapper)
        {
            _context = context;
            _autoMapper = autoMapper;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseDto>> Get()
        {
            try
            {
                var products = await _context.Products.ToListAsync();

                if (products == null || !products.Any())
                {
                    return new ResponseDto
                    {
                        IsSuccess = false,
                        Message = "No products found."
                    };
                }

                var responseDto = new ResponseDto
                {
                    Result = _autoMapper.Map<IEnumerable<ProductDto>>(products),
                };
                return Ok(responseDto);

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

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ResponseDto>> Get(int id)
        {
            try
            {
                var product = await _context.Products.FindAsync(id);
                if (product == null)
                {
                    return NotFound(new ResponseDto
                    {
                        IsSuccess = false,
                        Message = "Product not found."
                    });
                }
                var responseDto = new ResponseDto
                {
                    Result = _autoMapper.Map<ProductDto>(product),
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

        [HttpGet("GetListOfProductsByIds")]
        public async Task<ActionResult<ResponseDto>> GetListOfProductsByIds([FromBody] List<int> ids)
        {
            try
            {
                if (ids == null || !ids.Any())
                {
                    return BadRequest(new ResponseDto
                    {
                        IsSuccess = false,
                        Message = "No product IDs provided."
                    });
                }
                var products = await _context.Products.Where(p => ids.Contains(p.Id)).ToListAsync();
                if (products == null || !products.Any())
                {
                    return NotFound(new ResponseDto
                    {
                        IsSuccess = false,
                        Message = "No products found for the provided IDs."
                    });
                }
                var responseDto = new ResponseDto
                {
                    Result = _autoMapper.Map<IEnumerable<ProductDto>>(products),
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
        [Authorize(Roles = FixedResources.Admin)]
        public async Task<ActionResult<ResponseDto>> Create([FromBody] ProductDto productDto)
        {
            try
            {
                if (productDto == null)
                {
                    return BadRequest(new ResponseDto
                    {
                        IsSuccess = false,
                        Message = "Invalid product data."
                    });
                }
                var product = _autoMapper.Map<Product>(productDto);
                await _context.Products.AddAsync(product);
                await _context.SaveChangesAsync();
                var responseDto = new ResponseDto
                {
                    Result = _autoMapper.Map<ProductDto>(product),
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

        [HttpPut("{id:int}")]
        [Authorize(Roles = FixedResources.Admin)]
        public async Task<ActionResult<ResponseDto>> Update(int id, [FromBody] ProductDto productDto)
        {
            try
            {
                if (productDto == null || id != productDto.Id)
                {
                    return BadRequest(new ResponseDto
                    {
                        IsSuccess = false,
                        Message = "Invalid product data."
                    });
                }
                var existingProduct = await _context.Products.FindAsync(id);
                if (existingProduct == null)
                {
                    return NotFound(new ResponseDto
                    {
                        IsSuccess = false,
                        Message = "Product not found."
                    });
                }

                var updatedProduct = _autoMapper.Map(productDto, existingProduct);
                _context.Products.Update(updatedProduct);
                await _context.SaveChangesAsync();
                var responseDto = new ResponseDto
                {
                    Result = _autoMapper.Map<ProductDto>(existingProduct),
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
        [Authorize(Roles = FixedResources.Admin)]
        public async Task<ActionResult<ResponseDto>> Delete(int id)
        {
            try
            {
                var product = await _context.Products.FindAsync(id);
                if (product == null)
                {
                    return NotFound(new ResponseDto
                    {
                        IsSuccess = false,
                        Message = "Product not found."
                    });
                }
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
                return Ok(new ResponseDto
                {
                    Result = "Product deleted successfully."
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
