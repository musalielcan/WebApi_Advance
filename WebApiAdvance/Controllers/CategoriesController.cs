using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using WebApiAdvance.DAL.EFCore;
using WebApiAdvance.DAL.Repositories.Abstract;
using WebApiAdvance.DAL.UnitOfWork.Abstract;
using WebApiAdvance.Entities;
using WebApiAdvance.Entities.DTOs.Categories;

namespace WebApiAdvance.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public CategoriesController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<List<GetCategoryDto>>> GetAllCategories(int page=1, int size = 15)
        {
            var categories = await _unitOfWork.CategoryRepository.GetAllPaginatedAsync(page, size, null);
            var result = _mapper.Map<List<GetCategoryDto>>(categories);
            if (result == null || result.Count == 0)
            {
                return BadRequest(new
                {
                    Status = HttpStatusCode.BadRequest,
                    Message = "Kateqoriya tapılmadı"
                });
            }
            return StatusCode((int)HttpStatusCode.OK, result);
        }

        [HttpGet]
        public async Task<ActionResult<GetCategoryDto>> GetCategoryById(Guid id)
        {
            var category = await _unitOfWork.CategoryRepository.Get(c => c.Id == id);
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
            await _unitOfWork.CategoryRepository.AddAsync(category);
            await _unitOfWork.SaveAsync();
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCategory(Guid id, UpdateCategoryDto dto)
        {
            var category = await _unitOfWork.CategoryRepository.Get(c => c.Id == id);
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
            await _unitOfWork.SaveAsync();
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            var category = await _unitOfWork.CategoryRepository.Get(c => c.Id == id);
            if (category == null)
            {
                return BadRequest(new
                {
                    Status = HttpStatusCode.BadRequest,
                    Message = "Kateqoriya tapılmadı"
                });
            }
            _unitOfWork.CategoryRepository.Delete(category.Id);
            await _unitOfWork.SaveAsync();
            return Ok();
        }
    }
}
