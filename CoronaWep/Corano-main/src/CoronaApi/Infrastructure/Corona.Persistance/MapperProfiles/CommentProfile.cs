using AutoMapper;
using Corona.Application.DTOs.Auth;
using Corona.Application.DTOs.Comments;
using Corona.Domain.Entities;

namespace Corona.Persistance.MapperProfiles;

public class CommentProfile:Profile
{
    public CommentProfile()
    {
        CreateMap<Comment, CreateCommentDto>().ReverseMap();
        CreateMap<Comment, GetCommentDto>().ForMember(dest => dest.AppUserDto, opt => opt.MapFrom(src => src.AppUser))
            .ReverseMap();
        CreateMap<AppUser, AppUserDto>().ReverseMap();
    }
}
