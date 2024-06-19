using Corona.Application.DTOs.Comments;
using Corona.MVC.Service.Interfaces;
using Corona.MVC.ViewModel.Blog;
using Corona.MVC.ViewModel.Category;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Corona.MVC.Controllers;

public class BlogController : Controller
{
    private readonly ICrudService _crudService;
    Uri baseAddress = new Uri("https://localhost:7295/api");
    private readonly HttpClient _httpClient;
    public BlogController(ICrudService crudService, HttpClient httpClient)
    {
        _crudService = crudService;
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

    public async Task<IActionResult> BlogDetails(Guid Id)
    {
        var response = await _httpClient.GetAsync($"{_httpClient.BaseAddress}/Blogs/GetById/{Id}");
        if (response.IsSuccessStatusCode)
        {
            var stringData = await response.Content.ReadAsStringAsync();
            var blog = JsonConvert.DeserializeObject<BlogViewModel>(stringData);

            var categories = await _crudService.GetAllAsync<IEnumerable<CategoryViewModel>>("/Categorys/GetAll");
            ViewBag.Categories = categories;

            var commentsResponse = await _httpClient.GetAsync($"{_httpClient.BaseAddress}/Comments/GetAll/{Id}");
            if (commentsResponse.IsSuccessStatusCode)
            {
                var stringDataComments = await commentsResponse.Content.ReadAsStringAsync();
                var commentsObject = JsonConvert.DeserializeObject<IEnumerable<GetCommentDto>>(stringDataComments);
                ViewBag.Comments = commentsObject;
            }
            else
            {
                ViewBag.Comments = Enumerable.Empty<GetCommentDto>();
            }
            ViewBag.Token = Request.Cookies["token"];

            ViewBag.AppUserId = Request.Cookies["appUserId"];


            return View(blog);
        }
        return NotFound();
    }

}
