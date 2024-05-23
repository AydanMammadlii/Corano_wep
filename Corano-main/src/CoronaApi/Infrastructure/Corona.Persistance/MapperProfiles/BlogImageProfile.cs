using AutoMapper;
using Corona.Application.DTOs.BlogImages;
using Corona.Domain.Entities;

namespace Corona.Persistance.MapperProfiles;

public class BlogImageProfile : Profile
{
    public BlogImageProfile()
    {
        CreateMap<BlogImage, CreateBlogImageDto>().ReverseMap();
        CreateMap<BlogImage, GetBlogImageDto>().ReverseMap();
    }
}