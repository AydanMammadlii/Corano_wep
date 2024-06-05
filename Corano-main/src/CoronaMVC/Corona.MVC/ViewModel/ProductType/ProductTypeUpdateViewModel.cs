namespace Corona.MVC.ViewModel.ProductType;

public class ProductTypeUpdateViewModel
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public IFormFile ImageUrl { get; set; }
}
