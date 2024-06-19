namespace Corona.MVC.ViewModel.WishList;

public class WishlistViewModel
{
    public Guid ProductId { get; set; }
    public Guid WishlistId { get; set; }
    public double Price { get; set; }
    public string ImageUrl { get; set; }
    public string ProductTitle { get; set; }
}
