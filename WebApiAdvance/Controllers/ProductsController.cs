using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using WebApiAdvance.DAL.EFCore;
using WebApiAdvance.DAL.Repositories.Abstract;
using WebApiAdvance.DAL.Repositories.Concrete.EFCore;
using WebApiAdvance.DAL.UnitOfWork.Abstract;
using WebApiAdvance.Entities;
using WebApiAdvance.Entities.DTOs.Categories;
using WebApiAdvance.Entities.DTOs.Products;

namespace WebApiAdvance.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        
        IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public ProductsController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<List<GetProductDto>>> GetAllProducts(int page = 1, int size = 15)
        {
            var products = await _unitOfWork.ProductRepository.GetAllPaginatedAsync(page, size, null, "Category");
            var result = products.Select(p => new GetProductDto
            {
                Id = p.Id,
                Name = p.Name,
                CategoryName = p.Category.Name,
                Description = p.Description,
                Price = p.Price,
                DiscountPrice = p.DiscountPrice,
                Stock = p.Stock,
                SKU = p.SKU,
                Barcode = p.Barcode
            }).ToList();
            if (result == null || result.Count == 0)
            {
                return BadRequest(new
                {
                    Status = HttpStatusCode.BadRequest,
                    Message = "Məhsul tapılmadı"
                });
            }
            return StatusCode((int)HttpStatusCode.OK, result);
        }

        [HttpGet]
        public async Task<ActionResult<GetProductDto>> GetProductById(Guid id)
        {
            var product = await _unitOfWork.ProductRepository.Get(c => c.Id == id,"Category");
            var result = _mapper.Map<GetProductDto>(product);
            if (result == null)
            {
                return BadRequest(new
                {
                    Status = HttpStatusCode.BadRequest,
                    Message = "Mehsul tapılmadı"
                });
            }
            return StatusCode((int)HttpStatusCode.OK, result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductDto dto)
        {
            var product = _mapper.Map<Product>(dto);
            product.CreatedAt= DateTime.UtcNow;
            await _unitOfWork.ProductRepository.AddAsync(product);
            await _unitOfWork.SaveAsync();
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct(Guid id, UpdateProductDto dto)
        {
            var product = await _unitOfWork.ProductRepository.Get(c => c.Id == id);
            if (product == null)
            {
                return BadRequest(new
                {
                    Status = HttpStatusCode.BadRequest,
                    Message = "Məhsul tapılmadı"
                });
            }
            _mapper.Map(dto, product);
            product.UpdatedAt= DateTime.UtcNow;
            await _unitOfWork.SaveAsync();
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var product = await _unitOfWork.ProductRepository.Get(c => c.Id == id);
            if (product == null)
            {
                return BadRequest(new
                {
                    Status = HttpStatusCode.BadRequest,
                    Message = "Məhsul tapılmadı"
                });
            }
            _unitOfWork.ProductRepository.Delete(product.Id);
            await _unitOfWork.SaveAsync();
            return Ok();
        }
    }
}
