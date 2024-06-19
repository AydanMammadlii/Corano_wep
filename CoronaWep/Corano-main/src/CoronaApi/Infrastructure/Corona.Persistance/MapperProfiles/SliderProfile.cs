using AutoMapper;
using Corona.Application.DTOs.Slider;
using Corona.Domain.Entities;

namespace Corona.Persistance.MapperProfiles;

public class SliderProfile : Profile
{
    public SliderProfile()
    {
        CreateMap<Slider, SliderCreateDTO>().ReverseMap();
        CreateMap<Slider, SliderUpdateDTO>().ReverseMap();
        CreateMap<Slider, SliderGetDTO>().ReverseMap();
    }
}
