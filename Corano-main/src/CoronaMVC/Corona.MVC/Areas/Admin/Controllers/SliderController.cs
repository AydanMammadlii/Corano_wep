using Corona.MVC.ViewModel;
using Corona.MVC.ViewModel.Slider;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace Corona.MVC.Areas.Admin.Controllers;

[Area("Admin")]

public class SliderController : Controller
{
    Uri baseAddress = new Uri("https://localhost:7295/api");
    private readonly HttpClient _httpClient;
    public SliderController(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = baseAddress;
    }

    public async Task<IActionResult> Index()
    {
        List<SliderViewModel> model = new List<SliderViewModel>();
        var ressponseMessage = await _httpClient.GetAsync(baseAddress + "/Sliders");
        if (ressponseMessage.IsSuccessStatusCode)
        {
            var datas = await ressponseMessage.Content.ReadAsStringAsync();
            model = JsonConvert.DeserializeObject<List<SliderViewModel>>(datas);
        }
        return View(model);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(SliderCreateViewModel sliderCreateViewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(sliderCreateViewModel);
        }

        var dataStr = JsonConvert.SerializeObject(sliderCreateViewModel);
        var stringContent = new StringContent(dataStr, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(baseAddress + "/Sliders", stringContent);

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
        var response = await _httpClient.GetAsync($"{_httpClient.BaseAddress}/Sliders/{id}");
        if (response.IsSuccessStatusCode)
        {
            var stringData = await response.Content.ReadAsStringAsync();
            var slider = JsonConvert.DeserializeObject<SliderViewModel>(stringData);
            return View(slider);
        }
        return NotFound();
    }
}
