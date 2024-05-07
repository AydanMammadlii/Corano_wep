using AutoMapper;
using Corona.Application.DTOs.Testimonial;
using Corona.Domain.Entities;

namespace Corona.Persistance.MapperProfiles;

public class TestimonialProfile : Profile
{
    public TestimonialProfile()
    {
        CreateMap<Testimonials, TestimonialsGetDto>().ReverseMap();
        CreateMap<Testimonials, TestimonialsUpdateDto>().ReverseMap();
        CreateMap<Testimonials, TestimonialsCreateDto>().ReverseMap();
    }
}