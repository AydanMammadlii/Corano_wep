using Corona.MVC.Service.Interfaces;
using Corona.MVC.ViewModel.Blog;
using Corona.MVC.ViewModel.WishList;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Corona.MVC.Controllers;

public class WishlistController : Controller
{
    private readonly ICrudService _crudService;
    Uri baseAddress = new Uri("https://localhost:7295/api");
    private readonly HttpClient _httpClient;
    public WishlistController(ICrudService crudService, HttpClient httpClient)
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
        ViewBag.AppUserId = appUserId;


        List<WishlistViewModel> model = new List<WishlistViewModel>();

        var url = $"{baseAddress}/WishlistProducts?AppUserId={appUserId}";
        var ressponseMessage = await _httpClient.GetAsync(url);
        if (ressponseMessage.IsSuccessStatusCode)
        {
            var datas = await ressponseMessage.Content.ReadAsStringAsync();
            model = JsonConvert.DeserializeObject<List<WishlistViewModel>>(datas);
        }
        return View(model);

    }
}
