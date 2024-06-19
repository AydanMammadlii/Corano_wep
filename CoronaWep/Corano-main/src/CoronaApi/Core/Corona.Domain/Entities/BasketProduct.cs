using Corona.Domain.Entities.Common;

namespace Corona.Domain.Entities;

public class BasketProduct:BaseEntity
{
    public Guid ProductId { get; set; }
    public Product Product { get; set; }
    public Guid BasketId { get; set; }
    public Basket Basket { get; set; }
    public int Count { get; set; }
}
