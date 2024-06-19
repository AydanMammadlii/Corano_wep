using Corona.Domain.Entities.Common;

namespace Corona.Domain.Entities;

public class Brand:BaseEntity
{
    public string Title { get; set; }
    public ICollection<Category>? Categories { get; set; }
}
