using Corona.MVC.Models;
using Corona.MVC.Service.Interfaces;
using Corona.MVC.ViewModel;
using Corona.MVC.ViewModel.Blog;
using Corona.MVC.ViewModel.Home;
using Corona.MVC.ViewModel.Product;
using Corona.MVC.ViewModel.ProductType;
using Corona.MVC.ViewModel.Slider;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Corona.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICrudService _crudService;
        Uri baseAddress = new Uri("https://localhost:7295/api");
        private readonly HttpClient _httpClient;

        public HomeController(ICrudService crudService, HttpClient httpClient)
        {
            _crudService = crudService;
            _httpClient = httpClient;
            _httpClient.BaseAddress = baseAddress;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Token = Request.Cookies["token"];
            ViewBag.UserRole = Request.Cookies["UserRole"];
            ViewBag.Username = Request.Cookies["username"];

            List<SliderViewModel> sliders = new List<SliderViewModel>();
            var ressponseMessageSlider = await _httpClient.GetAsync(baseAddress + "/Sliders");
            if (ressponseMessageSlider.IsSuccessStatusCode)
            {
                var datasSlider = await ressponseMessageSlider.Content.ReadAsStringAsync();
                sliders = JsonConvert.DeserializeObject<List<SliderViewModel>>(datasSlider);
            }
            ViewBag.Sliders = sliders;

            List<ProductTypeViewModel> prodyctTypes = new List<ProductTypeViewModel>();
            var ressponseMessageProductType = await _httpClient.GetAsync(baseAddress + "/ProductTypes/GetAll");
            if (ressponseMessageProductType.IsSuccessStatusCode)
            {
                var datasPType = await ressponseMessageProductType.Content.ReadAsStringAsync();
                prodyctTypes = JsonConvert.DeserializeObject<List<ProductTypeViewModel>>(datasPType);
            }
            ViewBag.ProductTypes = prodyctTypes;

            var product = await _crudService.GetAllAsync<IEnumerable<ProductViewModel>>("/Products/GetAll");

            List<TestimonialsViewModel> modelTestimonial = new List<TestimonialsViewModel>();
            var ressponseMessageTestimonial = await _httpClient.GetAsync(baseAddress + "/Testimonials/GetAll");
            if (ressponseMessageTestimonial.IsSuccessStatusCode)
            {
                var datasTestimonial = await ressponseMessageTestimonial.Content.ReadAsStringAsync();
                modelTestimonial = JsonConvert.DeserializeObject<List<TestimonialsViewModel>>(datasTestimonial);
            }
            ViewBag.Testimonails = modelTestimonial;


            List<BlogViewModel> modelBlog = new List<BlogViewModel>();
            var ressponseMessageBlog = await _httpClient.GetAsync(baseAddress + "/Blogs/GetAll");
            if (ressponseMessageBlog.IsSuccessStatusCode)
            {
                var datasBlog = await ressponseMessageBlog.Content.ReadAsStringAsync();
                modelBlog = JsonConvert.DeserializeObject<List<BlogViewModel>>(datasBlog);
            }


            var homeViewModel = new HomeViewModel()
            {
                BlogViewModels = modelBlog,
                ProductViewModels = product
            };

            return View(homeViewModel);
        }


    }
}
