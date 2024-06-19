using Corona.MVC.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace Corona.MVC.Areas.Admin.Controllers;

[Area("Admin")]
public class DashboardController : Controller
{
    Uri baseAddress = new Uri("https://localhost:7295/api");
    private readonly HttpClient _httpClient;
    public DashboardController(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = baseAddress;
    }

    public async Task<IActionResult> Index()
    {
        List<TestimonialsViewModel> model = new List<TestimonialsViewModel>();
        var ressponseMessage = await _httpClient.GetAsync(baseAddress + "/Testimonials/GetAll");
        if (ressponseMessage.IsSuccessStatusCode)
        {
            var datas = await ressponseMessage.Content.ReadAsStringAsync();
            ViewBag.UserRole = Request.Cookies["UserRole"];
            model = JsonConvert.DeserializeObject<List<TestimonialsViewModel>>(datas);
        }
        return View(model);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(TestimonialsCreateViewModel testimonialsCreateViewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(testimonialsCreateViewModel);
        }

        var dataStr = JsonConvert.SerializeObject(testimonialsCreateViewModel);
        var stringContent = new StringContent(dataStr, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(baseAddress + "/Testimonials/CreateTestimonial", stringContent);

        if (response.IsSuccessStatusCode)
        {
            TempData["Successed"] = "Testimonial successfully create";
            return RedirectToAction("Index");
        }
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> Update(Guid id)
    {
        var response = await _httpClient.GetAsync($"{_httpClient.BaseAddress}/Testimonials/GetById/{id}");
        if (response.IsSuccessStatusCode)
        {
            var stringData = await response.Content.ReadAsStringAsync();
            var testimonial = JsonConvert.DeserializeObject<TestimonialsViewModel>(stringData);
            return View(testimonial);
        }
        return NotFound();
    }

    
}

