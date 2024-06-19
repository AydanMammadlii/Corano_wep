using Corona.Domain.Entities.Common;

namespace Corona.Domain.Entities;

public class Blog:BaseEntity
{
    public string Title { get; set; }
    public string Description { get; set; }

    public ICollection<BlogImage>? BlogImages { get; set; }
    public ICollection<Comment>? Comments { get; set; }
}
