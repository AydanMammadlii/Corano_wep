using Corona.Domain.Entities.Common;

namespace Corona.Domain.Entities;

public class Product : BaseEntity
{
    public string Title { get; set; }
    public string? Description { get; set; }
    public int StockCount { get; set; }
    public string Size { get; set; }
    public string Color { get; set; }
    public double Price { get; set; }
    public ICollection<ProductImage>? ProductsImages { get; set; }
    public Guid ProductTypeId { get; set; }
    public ProductType ProductType { get; set; }
    public Guid CategoryId { get; set; }
    public Category Category { get; set; }
    public ICollection<BasketProduct>? BasketProducts { get; set; }
    public ICollection<WishlistProduct>? WishlistProducts { get; set; }
}