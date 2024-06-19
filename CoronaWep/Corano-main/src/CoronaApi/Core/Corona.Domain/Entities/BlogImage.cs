using Corona.Domain.Entities.Common;

namespace Corona.Domain.Entities;

public class BlogImage:BaseEntity
{
    public string ImageUrl { get; set; }
    public Guid BlogId { get; set; }
    public Blog Blog { get; set; }
}
