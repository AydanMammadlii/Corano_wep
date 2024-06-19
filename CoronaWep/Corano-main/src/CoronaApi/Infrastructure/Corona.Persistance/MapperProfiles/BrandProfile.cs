using AutoMapper;
using Corona.Application.DTOs.Brands;
using Corona.Domain.Entities;

namespace Corona.Persistance.MapperProfiles;

public class BrandProfile : Profile
{
    public BrandProfile()
    {
        CreateMap<Brand, CreateBrandDto>().ReverseMap();
        CreateMap<Brand, UpdateBrandDto>().ReverseMap();
        CreateMap<Brand, GetBrandDto>()
                        .ForMember(dest => dest.GetCategoryDtos, opt => opt.MapFrom(src => src.Categories))
                        .ReverseMap();
    }
}
