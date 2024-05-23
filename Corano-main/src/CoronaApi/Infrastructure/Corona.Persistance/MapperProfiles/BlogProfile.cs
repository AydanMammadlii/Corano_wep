using AutoMapper;
using Corona.Application.DTOs.Blogs;
using Corona.Domain.Entities;

namespace Corona.Persistance.MapperProfiles;

public class BlogProfile : Profile
{
    public BlogProfile()
    {
        CreateMap<Blog, CreateBlogDto>().ReverseMap();
        CreateMap<Blog, UpdateBlogDto>().ReverseMap();
        CreateMap<Blog, GetBlogDto>()
            .ForMember(dest => dest.GetBlogImageDtos, opt => opt.MapFrom(src => src.BlogImages))
            .ReverseMap();
    }
}