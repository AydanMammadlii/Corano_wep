using Corona.Application.DTOs.Comments;

namespace Corona.Application.Abstraction.Services;

public interface ICommentService
{
    Task<List<GetCommentDto>> GetAllAsync(Guid BlogId);
    Task CreateAsync(CreateCommentDto createCommentDto);
    Task RemoveAsync(RemoveCommentDto removeCommentDto);
}