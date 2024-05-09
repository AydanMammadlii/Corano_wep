using Corona.Domain.Entities.Common;

namespace Corona.Domain.Entities;

public class WishlistProduct : BaseEntity
{
    public Guid ProductId { get; set; }
    public Product Product { get; set; }
    public Guid WishlistId { get; set; }
    public Wishlist Wishlist { get; set; }
}