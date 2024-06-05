using Corona.Application.DTOs.ProductsImages;

namespace Corona.MVC.ViewModel.Product;

public class ProductViewModel
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public int StockCount { get; set; }
    public string Size { get; set; }
    public string Color { get; set; }
    public double Price { get; set; }
    public ICollection<GetProductsImageDto>? GetProductsImages { get; set; }
    public Guid ProductTypeId { get; set; }
    public Guid CategoryId { get; set; }
    public List<IFormFile> UpdateProductImageDto { get; set; }

}
