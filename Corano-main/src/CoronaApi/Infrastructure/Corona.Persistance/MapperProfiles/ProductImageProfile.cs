using AutoMapper;
using Corona.Application.DTOs.ProductsImages;
using Corona.Domain.Entities;

namespace Corona.Persistance.MapperProfiles;

public class ProductImageProfile : Profile
{
    public ProductImageProfile()
    {
        CreateMap<ProductImage, CreateProductsImageDto>().ReverseMap();
        CreateMap<ProductImage, GetProductsImageDto>().ReverseMap();
    }
}