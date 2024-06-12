namespace Corona.MVC.ViewModel.Shop;

public class ProductSearchViewModel
{
    public string? Category { get; set; }
    public List<string>? Brands { get; set; }
    public List<string>? Colors { get; set; }
    public List<string>? Sizes { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 20;
    public int? minPrice { get; set; } = null;
    public int? maxPrice { get; set; } = null;
    public int TotalItems { get; set; }
}
