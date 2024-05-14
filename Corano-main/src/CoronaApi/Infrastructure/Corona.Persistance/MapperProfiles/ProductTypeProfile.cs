using AutoMapper;
using Corona.Application.DTOs.ProductTypes;
using Corona.Domain.Entities;

namespace Corona.Persistance.MapperProfiles;

public class ProductTypeProfile : Profile
{
    public ProductTypeProfile()
    {
        CreateMap<ProductType, CreateProductTypeDto>().ReverseMap();
        CreateMap<ProductType, GetProductTypeDto>().ReverseMap();
        CreateMap<ProductType, UpdateProductTypeDto>().ReverseMap();
    }
}