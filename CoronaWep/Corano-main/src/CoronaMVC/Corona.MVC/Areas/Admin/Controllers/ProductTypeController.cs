using Corona.MVC.ViewModel.ProductType;
using Corona.MVC.ViewModel.Slider;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace Corona.MVC.Areas.Admin.Controllers;

[Area("Admin")]
public class ProductTypeController : Controller
{
    Uri baseAddress = new Uri("https://localhost:7295/api");
    private readonly HttpClient _httpClient;
    public ProductTypeController(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = baseAddress;
    }

    public async Task<IActionResult> Index()
    {
        List<ProductTypeViewModel> model = new List<ProductTypeViewModel>();
        var ressponseMessage = await _httpClient.GetAsync(baseAddress + "/ProductTypes/GetAll");
        if (ressponseMessage.IsSuccessStatusCode)
        {
            var datas = await ressponseMessage.Content.ReadAsStringAsync();
            model = JsonConvert.DeserializeObject<List<ProductTypeViewModel>>(datas);
        }
        return View(model);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(ProductTypeCreateViewModel productTypeCreateViewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(productTypeCreateViewModel);
        }

        var dataStr = JsonConvert.SerializeObject(productTypeCreateViewModel);
        var stringContent = new StringContent(dataStr, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(baseAddress + "/ProductTypes/CreateProductType", stringContent);

        if (response.IsSuccessStatusCode)
        {
            TempData["Successed"] = "Product Type successfully create";
            return RedirectToAction("https://localhost:7186/Admin/ProductType");
        }
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> Update(Guid id)
    {
        var response = await _httpClient.GetAsync($"{_httpClient.BaseAddress}/ProductTypes/GetById/{id}");
        if (response.IsSuccessStatusCode)
        {
            var stringData = await response.Content.ReadAsStringAsync();
            var productType = JsonConvert.DeserializeObject<ProductTypeViewModel>(stringData);
            return View(productType);
        }
        return NotFound();
    }
}
