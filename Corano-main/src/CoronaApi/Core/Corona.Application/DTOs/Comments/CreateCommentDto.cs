namespace Corona.Application.DTOs.Comments;

public class CreateCommentDto
{
    public string CommentText { get; set; }
    public Guid BlogId { get; set; }
    public string AppUserId { get; set; }
}