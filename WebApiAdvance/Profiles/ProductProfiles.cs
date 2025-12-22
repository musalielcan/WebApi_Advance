using AutoMapper;
using WebApiAdvance.Entities;
using WebApiAdvance.Entities.DTOs.Products;

namespace WebApiAdvance.Profiles
{
    public class ProductProfiles:Profile
    {
        public ProductProfiles()
        {
            CreateMap<Product, GetProductDto>().ReverseMap();
            CreateMap<Product, CreateProductDto>().ReverseMap();
            CreateMap<Product, UpdateProductDto>().ReverseMap();
        }
    }
}
