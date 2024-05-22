using Corona.Domain.Entities.Common;

namespace Corona.Domain.Entities;

public class Comment : BaseEntity
{
    public string CommentText { get; set; }
    public Guid BlogId { get; set; }
    public Blog Blog { get; set; }
    public string AppUserId { get; set; }
    public AppUser AppUser { get; set; }
}