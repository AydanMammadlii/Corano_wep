using Corona.Domain.Entities;
using Corona.MVC.ViewModel.Blog;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Corona.MVC.Areas.Admin.Controllers;

[Area("Admin")]
public class AdminController : Controller
{
    Uri baseAddress = new Uri("https://localhost:7295/api");
    private readonly HttpClient _httpClient;
    public AdminController(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = baseAddress;
    }
    public async Task<IActionResult> Index()
    {
        List<AppUser> model = new List<AppUser>();
        var ressponseMessage = await _httpClient.GetAsync(baseAddress + "/Auth/AllMember");
        if (ressponseMessage.IsSuccessStatusCode)
        {
            var datas = await ressponseMessage.Content.ReadAsStringAsync();
            model = JsonConvert.DeserializeObject<List<AppUser>>(datas);
        }
        ViewBag.SuperAdmin = Request.Cookies["appUserId"];
        return View(model); 
    }

    public async Task<IActionResult> IndexAllAdmin()
    {
        ViewBag.SuperAdmin = Request.Cookies["appUserId"];
        ViewBag.Username = Request.Cookies["username"];
        List<AppUser> model = new List<AppUser>();
        var ressponseMessage = await _httpClient.GetAsync(baseAddress + "/Auth/AllAdmin");
        if (ressponseMessage.IsSuccessStatusCode)
        {
            var datas = await ressponseMessage.Content.ReadAsStringAsync();
            model = JsonConvert.DeserializeObject<List<AppUser>>(datas);
        }
        return View(model);
    }
}
