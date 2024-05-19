using AutoMapper;
using Corona.Application.DTOs.BasketProducts;
using Corona.Domain.Entities;

namespace Corona.Persistance.MapperProfiles;

public class BasketProductProfile : Profile
{
    public BasketProductProfile()
    {
        CreateMap<BasketProduct, CreateBasketProductDto>().ReverseMap();
        CreateMap<BasketProduct, UpdateBasketProductDto>().ReverseMap();
        CreateMap<BasketProduct, GetBasketProductDto>().ReverseMap();
    }
}