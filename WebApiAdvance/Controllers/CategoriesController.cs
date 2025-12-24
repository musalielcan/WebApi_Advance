using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using WebApiAdvance.DAL.EFCore;
using WebApiAdvance.Entities;
using WebApiAdvance.Entities.DTOs.Categories;

namespace WebApiAdvance.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ApiDbContext _context;
        IMapper _mapper;
        public CategoriesController(ApiDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<GetCategoryDto>>> GetAllCategories()
        {
            var categories = await _context.Categories
                .Select(c => _mapper.Map<GetCategoryDto>(c))
                .ToListAsync();
            if (categories == null || categories.Count == 0)
            {
                return BadRequest(new
                {
                    Status = HttpStatusCode.BadRequest,
                    Message = "Kateqoriya tapılmadı"
                });
            }
            return StatusCode((int)HttpStatusCode.OK, categories);
        }

        [HttpGet]
        public async Task<ActionResult<GetCategoryDto>> GetCategoryById(Guid id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            var dto = _mapper.Map<GetCategoryDto>(category);
            if (dto == null)
            {
                return BadRequest(new
                {
                    Status = HttpStatusCode.BadRequest,
                    Message = "Kateqoriya tapılmadı"
                });
            }
            return StatusCode((int)HttpStatusCode.OK, dto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryDto dto)
        {
            var category = _mapper.Map<Category>(dto);
            category.CreatedAt = DateTime.UtcNow;
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCategory(Guid id, UpdateCategoryDto dto)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (category == null)
            {
                return BadRequest(new
                {
                    Status = HttpStatusCode.BadRequest,
                    Message = "Kateqoriya tapılmadı"
                });

            }
            _mapper.Map(dto, category);
            category.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (category == null)
            {
                return BadRequest(new
                {
                    Status = HttpStatusCode.BadRequest,
                    Message = "Kateqoriya tapılmadı"
                });
            }
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
