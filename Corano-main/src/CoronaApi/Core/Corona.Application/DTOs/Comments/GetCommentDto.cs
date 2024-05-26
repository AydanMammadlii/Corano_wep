using Corona.Application.DTOs.Auth;

namespace Corona.Application.DTOs.Comments;

public class GetCommentDto
{
    public Guid Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public string CommentText { get; set; }
    public Guid BlogId { get; set; }
    public string AppUserId { get; set; }
    public AppUserDto AppUserDto { get; set; }
}