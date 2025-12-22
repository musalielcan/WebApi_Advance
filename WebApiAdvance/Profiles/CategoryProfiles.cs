using AutoMapper;
using WebApiAdvance.Entities;
using WebApiAdvance.Entities.DTOs.Categories;

namespace WebApiAdvance.Profiles
{
    public class CategoryProfiles:Profile
    {
        public CategoryProfiles()
        {
            CreateMap<Category, GetCategoryDto>().ReverseMap();
            CreateMap<Category, CreateCategoryDto>().ReverseMap();
            CreateMap<Category, UpdateCategoryDto>().ReverseMap();
        }
    }
}
