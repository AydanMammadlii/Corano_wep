using Corona.MVC.Service.Interfaces;
using Corona.MVC.ViewModel.Basket;
using Corona.MVC.ViewModel.Product;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Corona.MVC.Controllers;

public class BasketController : Controller
{
    private readonly ICrudService _crudService;
    Uri baseAddress = new Uri("https://localhost:7295/api");
    private readonly HttpClient _httpClient;

    public BasketController(ICrudService crudService, HttpClient httpClient)
    {
        _crudService = crudService;
        _httpClient = httpClient;
        _httpClient.BaseAddress = baseAddress;
    }
    public async Task<IActionResult> Index()
    {
        if (HttpContext.Request.Cookies["token"] is null)
        {
            return Redirect("https://localhost:7186/auth/login");
        }
        var appUserId = Request.Cookies["appUserId"];

        // API endpoint URL'si
        var url = $"{baseAddress}/BasketProducts/GetAll?AppUserId={appUserId}";

        var responseMessage = await _httpClient.GetAsync(url);

        if (responseMessage.IsSuccessStatusCode)
        {
            // Yanıt içeriğini JSON string olarak al
            var jsonData = await responseMessage.Content.ReadAsStringAsync();

            // JSON stringini model listesine deserializing yap
            var data = JsonConvert.DeserializeObject<BasketResultViewModel>(jsonData);

            // Modeli View'e döndür
            return View(data);
        }

        return View(new BasketResultViewModel());
    }
}
