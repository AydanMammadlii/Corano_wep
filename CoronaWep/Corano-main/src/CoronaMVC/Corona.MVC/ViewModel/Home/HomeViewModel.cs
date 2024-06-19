using Corona.MVC.ViewModel.Blog;
using Corona.MVC.ViewModel.Product;

namespace Corona.MVC.ViewModel.Home;

public class HomeViewModel
{
    public List<BlogViewModel> BlogViewModels { get; set; }
    public IEnumerable<ProductViewModel> ProductViewModels { get; set; }
}
