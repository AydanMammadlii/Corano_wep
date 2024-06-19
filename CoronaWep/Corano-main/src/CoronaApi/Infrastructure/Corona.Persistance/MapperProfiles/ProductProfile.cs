using AutoMapper;
using Corona.Application.DTOs.Products;
using Corona.Domain.Entities;

namespace Corona.Persistance.MapperProfiles;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, CreateProductDto>().ReverseMap();
        CreateMap<Product, UpdateProductDto>().ReverseMap();
        CreateMap<Product, GetProductDto>()
                        .ForMember(dest => dest.GetProductsImages, opt => opt.MapFrom(src => src.ProductsImages))
                        .ReverseMap();
    }
}
