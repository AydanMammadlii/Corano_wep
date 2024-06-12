﻿namespace Corona.MVC.ViewModel.Shop;

public class PagedResult<T>
{
    public List<T> Items { get; set; }
    public int TotalItems { get; set; }
}
