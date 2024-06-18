﻿namespace Corona.MVC.ViewModel.Basket;

public class GetBasketProductViewModel
{
    public Guid ProductId { get; set; }
    public Guid BasketId { get; set; }
    public int Count { get; set; }
    public double Price { get; set; }
    public string ImageUrl { get; set; }
    public string ProductTitle { get; set; }
}
