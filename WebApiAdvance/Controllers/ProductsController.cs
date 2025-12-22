using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using WebApiAdvance.DAL.EFCore;
using WebApiAdvance.Entities.DTOs.Products;

namespace WebApiAdvance.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ApiDbContext _context;
        IMapper _mapper;
        public ProductsController(ApiDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<GetProductDto>>> GetAllProducts()
        {
            var products= await _context.Products
                .Select(p => _mapper.Map<GetProductDto>(p))
                .ToListAsync();
            if(products == null || products.Count == 0)
            {
                return BadRequest(new
                {
                    Status = HttpStatusCode.BadRequest,
                    Message = "Product not found"
                });
            }
            return StatusCode((int)HttpStatusCode.OK, products);
        }

        [HttpGet]
        public async Task<ActionResult<GetProductDto>> GetProductById(Guid id)
        {
            var dto=await _context.Products.Where(p => p.Id == id)
                .Select(p => _mapper.Map<GetProductDto>(p))
                .FirstOrDefaultAsync();
            if (dto == null)
            {
                return BadRequest(new
                {
                    Status = HttpStatusCode.BadRequest,
                    Message = "Product not found"
                });
            }
            return StatusCode((int)HttpStatusCode.OK, dto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductDto dto)
        {
            var product = _mapper.Map<Entities.Product>(dto);
            product.CreatedAt= DateTime.UtcNow;
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct(Guid id, UpdateProductDto dto)
        {
            var product= await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (product == null)
            {
                return BadRequest(new
                {
                    Status = HttpStatusCode.BadRequest,
                    Message = "Product not found"
                });
            }
            _mapper.Map(dto, product);
            product.UpdatedAt= DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (product == null)
            {
                return BadRequest(new
                {
                    Status = HttpStatusCode.BadRequest,
                    Message = "Product not found"
                });
            }
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
