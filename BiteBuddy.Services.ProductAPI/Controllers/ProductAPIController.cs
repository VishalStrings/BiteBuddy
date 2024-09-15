using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BiteBuddy.Services.ProductAPI.Models;
using BiteBuddy.Services.ProductAPI.Models.Dto;
using BiteBuddy.Services.ProductAPI.Data;

namespace BiteBuddy.Services.ProductAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProductAPIController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ProductAPI
        [HttpGet]
        public async Task<ActionResult<ResponseDto>> GetProducts()
        {
            var products = await _context.Products.ToListAsync();
            return Ok(new ResponseDto
            {
                Result = products,
                IsSuccess = true
            });
        }

        // GET: api/ProductAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDto>> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound(new ResponseDto
                {
                    Result = null,
                    IsSuccess = false,
                    Message = "Product not found"
                });
            }

            return Ok(new ResponseDto
            {
                Result = product,
                IsSuccess = true
            });
        }

        // POST: api/ProductAPI
        [HttpPost]
        public async Task<ActionResult<ResponseDto>> CreateProduct(Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ResponseDto
                {
                    Result = null,
                    IsSuccess = false,
                    Message = "Invalid model state"
                });
            }

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProduct), new { id = product.ProductId }, new ResponseDto
            {
                Result = product,
                IsSuccess = true
            });
        }

        // PUT: api/ProductAPI/5
        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseDto>> UpdateProduct(int id, Product product)
        {
            if (id != product.ProductId)
            {
                return BadRequest(new ResponseDto
                {
                    Result = null,
                    IsSuccess = false,
                    Message = "Product ID mismatch"
                });
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound(new ResponseDto
                    {
                        Result = null,
                        IsSuccess = false,
                        Message = "Product not found"
                    });
                }
                else
                {
                    throw;
                }
            }

            return Ok(new ResponseDto
            {
                Result = product,
                IsSuccess = true
            });
        }

        // DELETE: api/ProductAPI/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseDto>> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound(new ResponseDto
                {
                    Result = null,
                    IsSuccess = false,
                    Message = "Product not found"
                });
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return Ok(new ResponseDto
            {
                Result = product,
                IsSuccess = true
            });
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }
    }
}
