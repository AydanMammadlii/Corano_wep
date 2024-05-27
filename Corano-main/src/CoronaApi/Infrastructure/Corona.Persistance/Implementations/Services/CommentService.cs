using AutoMapper;
using Corona.Application.Abstraction.Repositories.IEntityRepository;
using Corona.Application.Abstraction.Services;
using Corona.Application.DTOs.Comments;
using Corona.Domain.Entities;
using Corona.Persistance.Context;
using Corona.Persistance.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Corona.Persistance.Implementations.Services;

public class CommentService : ICommentService
{
    private readonly IMapper _mapper;
    private readonly ICommentReadRepository _commentReadRepository;
    private readonly ICommentWriteRepository _commentWriteRepository;
    private readonly AppDbContext _appDbContext;

    public CommentService(IMapper mapper, ICommentReadRepository commentReadRepository, ICommentWriteRepository commentWriteRepository, AppDbContext appDbContext)
    {
        _mapper = mapper;
        _commentReadRepository = commentReadRepository;
        _commentWriteRepository = commentWriteRepository;
        _appDbContext = appDbContext;
    }

    public async Task CreateAsync(CreateCommentDto createCommentDto)
    {
        var user = await _appDbContext.AppUsers.FirstOrDefaultAsync(x => x.Id == createCommentDto.AppUserId);
        if (user is null) throw new NotFoundException("Not found user");

        var blog = await _appDbContext.Blogs.FirstOrDefaultAsync(x => x.Id == createCommentDto.BlogId);
        if (blog is null) throw new NotFoundException("Not found blog");

        var newComment = _mapper.Map<Comment>(createCommentDto);
        await _commentWriteRepository.AddAsync(newComment);
        await _commentWriteRepository.SaveChangeAsync();
    }

    public async Task<List<GetCommentDto>> GetAllAsync(Guid BlogId)
    {
        var blog = await _appDbContext.Blogs.FirstOrDefaultAsync(x => x.Id == BlogId);
        if (blog is null) throw new NotFoundException("Not found blog");

        var comments = await _appDbContext.Comments.Include(x => x.AppUser).ToListAsync();
        var toDto = _mapper.Map<List<GetCommentDto>>(comments);
        return toDto;
    }

    public async Task RemoveAsync(RemoveCommentDto removeCommentDto)
    {
        var comment = await _commentReadRepository.GetByIdAsync(removeCommentDto.Id);
        if (comment is null) throw new NotFoundException("Comment not found");

        if (comment.AppUserId != removeCommentDto.AppUserId) throw new Exception("Not Access");

        _commentWriteRepository.Remove(comment);
        await _commentWriteRepository.SaveChangeAsync();
    }
}