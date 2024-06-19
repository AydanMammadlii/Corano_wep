using Corona.Domain.Entities.Common;

namespace Corona.Domain.Entities;

public class ProductType:BaseEntity
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public ICollection<Product>? Products { get; set; }
}
