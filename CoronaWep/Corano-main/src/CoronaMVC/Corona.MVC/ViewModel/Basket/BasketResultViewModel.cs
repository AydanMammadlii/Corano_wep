using Corona.Application.DTOs.BasketProducts;

namespace Corona.MVC.ViewModel.Basket;

public class BasketResultViewModel
{
    public List<GetBasketProductViewModel> GetBasketProductDto { get; set; }
    public double TotalPrice { get; set; }
}
