using Corona.Domain.Entities.Common;

namespace Corona.Domain.Entities;

public class ProductImage : BaseEntity
{
    public string ImageUrl { get; set; }
    public Guid ProductId { get; set; }
    public Product Product { get; set; }
}