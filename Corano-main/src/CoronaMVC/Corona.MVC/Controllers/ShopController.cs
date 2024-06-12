using Corona.Application.Abstraction.Services;
using Corona.Application.DTOs.Products;
using Corona.MVC.Service.Interfaces;
using Corona.MVC.ViewModel.Blog;
using Corona.MVC.ViewModel.Brand;
using Corona.MVC.ViewModel.Category;
using Corona.MVC.ViewModel.Product;
using Corona.MVC.ViewModel.Shop;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Corona.MVC.Controllers;

public class ShopController : Controller
{
    private readonly ICrudService _crudService;
    Uri baseAddress = new Uri("https://localhost:7295/api");
    private readonly HttpClient _httpClient;
    public ShopController(ICrudService crudService, HttpClient httpClient)
    {
        _crudService = crudService;
        _httpClient = httpClient;
        _httpClient.BaseAddress = baseAddress;
    }
    public async Task<IActionResult> Index(ProductSearchViewModel model)
    {
        var categorys = await _crudService.GetAllAsync<IEnumerable<CategoryViewModel>>("/Categorys/GetAll");
        ViewBag.Categorys = categorys;

        var brands = await _crudService.GetAllAsync<IEnumerable<BrandViewModel>>("/Brands/GetAll");
        ViewBag.Brands = brands;

        var appUserId = Request.Cookies["appUserId"];
        ViewBag.AppUserId = appUserId;

        List<ProductViewModel> products = new List<ProductViewModel>();
        var url = $"{baseAddress}/Products/GetAll?Category={model.Category}";

        if (model.Brands != null && model.Brands.Any())
        {
            url += $"&Brand={string.Join(",", model.Brands)}";
        }

        if (model.Colors != null && model.Colors.Any())
        {
            url += $"&Color={string.Join(",", model.Colors)}";
        }

        if (model.Sizes != null && model.Sizes.Any())
        {
            url += $"&Size={string.Join(",", model.Sizes)}";
        }

        url += $"&pageNumber={model.PageNumber}&pageSize={model.PageSize}&minPrice={model.minPrice}&maxPrice={model.maxPrice}"; var ressponseMessage = await _httpClient.GetAsync(url);

        if (ressponseMessage.IsSuccessStatusCode)
        {
            var datas = await ressponseMessage.Content.ReadAsStringAsync();
            products = JsonConvert.DeserializeObject<List<ProductViewModel>>(datas);
        }

        ViewBag.Products = products;

        var basketId = Request.Cookies["basketId"];
        ViewBag.BasketId = basketId;
        return View();
    }

    public async Task<IActionResult> ProductDetails(Guid id)
    {
        var response = await _httpClient.GetAsync($"{_httpClient.BaseAddress}/Products/GetById/{id}");
        if (response.IsSuccessStatusCode)
        {
            var stringData = await response.Content.ReadAsStringAsync();
            var product = JsonConvert.DeserializeObject<ProductViewModel>(stringData);

            var datas = await _crudService.GetAllAsync<IEnumerable<ProductViewModel>>("/Products/GetAll");
            ViewBag.DataProduct = datas;

            var basketId = Request.Cookies["basketId"];
            ViewBag.BasketId = basketId;

            var appUserId = Request.Cookies["appUserId"];
            ViewBag.AppUserId = appUserId;

            return View(product);
        }
        return NotFound();
    }
}
