using Corona.MVC.ViewModel.Blog;
using Corona.MVC.ViewModel.ProductType;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace Corona.MVC.Areas.Admin.Controllers;

[Area("Admin")]
public class BlogController : Controller
{
    Uri baseAddress = new Uri("https://localhost:7295/api");
    private readonly HttpClient _httpClient;
    public BlogController(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = baseAddress;
    }

    public async Task<IActionResult> Index()
    {
        List<BlogViewModel> model = new List<BlogViewModel>();
        var ressponseMessage = await _httpClient.GetAsync(baseAddress + "/Blogs/GetAll");
        if (ressponseMessage.IsSuccessStatusCode)
        {
            var datas = await ressponseMessage.Content.ReadAsStringAsync();
            model = JsonConvert.DeserializeObject<List<BlogViewModel>>(datas);
        }
        return View(model);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(BlogCreateViewModel blogCreateViewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(blogCreateViewModel);
        }

        var dataStr = JsonConvert.SerializeObject(blogCreateViewModel);
        var stringContent = new StringContent(dataStr, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(baseAddress + "/Blogs/CreateBlog", stringContent);

        if (response.IsSuccessStatusCode)
        {
            TempData["Successed"] = "Blog successfully create";
            return RedirectToAction("https://localhost:7186/Admin/Blog");
        }
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> Update(Guid id)
    {
        var response = await _httpClient.GetAsync($"{_httpClient.BaseAddress}/Blogs/GetById/{id}");
        if (response.IsSuccessStatusCode)
        {
            var stringData = await response.Content.ReadAsStringAsync();
            var blog = JsonConvert.DeserializeObject<BlogViewModel>(stringData);
            return View(blog);
        }
        return NotFound();
    }
}
