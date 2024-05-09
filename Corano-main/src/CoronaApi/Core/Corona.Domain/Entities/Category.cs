using Corona.Domain.Entities.Common;

namespace Corona.Domain.Entities;

public class Category : BaseEntity
{
    public string Title { get; set; }
    public ICollection<Product>? Products { get; set; }
    public Guid BrandId { get; set; }
    public Brand Brand { get; set; }
}