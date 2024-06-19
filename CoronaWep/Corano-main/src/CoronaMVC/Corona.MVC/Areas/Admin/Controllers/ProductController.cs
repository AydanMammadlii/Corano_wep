using Corona.MVC.ExamProgramUIExceptions;
using Corona.MVC.Service.Interfaces;
using Corona.MVC.ViewModel.Brand;
using Corona.MVC.ViewModel.Category;
using Corona.MVC.ViewModel.Product;
using Corona.MVC.ViewModel.ProductType;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Corona.MVC.Areas.Admin.Controllers;

[Area("Admin")]
public class ProductController : Controller
{
    private readonly ICrudService _crudService;
    Uri baseAddress = new Uri("https://localhost:7295/api");
    private readonly HttpClient _httpClient;

    public ProductController(ICrudService crudService, HttpClient httpClient)
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
        var datas = await _crudService.GetAllAsync<IEnumerable<ProductViewModel>>("/Products/GetAll");

        return View(datas);
    }

    [HttpGet]
    public async Task<IActionResult> GetProductDetails(Guid id)
    {
        var response = await _httpClient.GetAsync($"{_httpClient.BaseAddress}/Products/GetById/{id}");
        if (response.IsSuccessStatusCode)
        {
            var stringData = await response.Content.ReadAsStringAsync();
            var product = JsonConvert.DeserializeObject<ProductViewModel>(stringData);

            var responseCategory = await _httpClient.GetAsync($"{_httpClient.BaseAddress}/Categorys/GetById/{product.CategoryId}");
            var stringDataCategory = await responseCategory.Content.ReadAsStringAsync();
            var category = JsonConvert.DeserializeObject<CategoryViewModel>(stringDataCategory);
            ViewBag.Category = category;

            var responseProductType = await _httpClient.GetAsync($"{_httpClient.BaseAddress}/ProductTypes/GetById/{product.ProductTypeId}");
            var stringDataProductType = await responseProductType.Content.ReadAsStringAsync();
            var productType = JsonConvert.DeserializeObject<ProductTypeViewModel>(stringDataProductType);
            ViewBag.ProductType = productType;

            return View(product);
        }
        return NotFound();
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        if (HttpContext.Request.Cookies["token"] is null)
        {
            return Redirect("https://localhost:7186/auth/login");
        }
        var datasCategory = await _crudService.GetAllAsync<IEnumerable<CategoryViewModel>>("/Categorys/GetAll");
        ViewBag.Category = datasCategory;

        if (HttpContext.Request.Cookies["token"] is null)
        {
            return Redirect("https://localhost:7186/auth/login");
        }
        var datasProductType = await _crudService.GetAllAsync<IEnumerable<ProductTypeViewModel>>("/ProductTypes/GetAll");
        ViewBag.ProductType = datasProductType;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(ProductCreateViewModel model)
    {
        try
        {
            await _crudService.CreateAsync("/Brands/CreateBrand", model);
        }
        catch (ApiException ex)
        {
            if (ex.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                foreach (var key in ex.ModelErrors.Keys)
                {
                    ModelState.AddModelError(key, ex.ModelErrors[key]);
                }
                return View(model);
            }
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return View();
        }

        return RedirectToAction(nameof(Index));
    }


    [HttpGet]
    public async Task<IActionResult> Update(Guid id)
    {
        var datasCategory = await _crudService.GetAllAsync<IEnumerable<CategoryViewModel>>("/Categorys/GetAll");
        ViewBag.Category = datasCategory;

        var datasProductType = await _crudService.GetAllAsync<IEnumerable<ProductTypeViewModel>>("/ProductTypes/GetAll");
        ViewBag.ProductType = datasProductType;

        var response = await _httpClient.GetAsync($"{_httpClient.BaseAddress}/Products/GetById/{id}");
        if (response.IsSuccessStatusCode)
        {
            var stringData = await response.Content.ReadAsStringAsync();
            var brand = JsonConvert.DeserializeObject<ProductViewModel>(stringData);
            return View(brand);
        }
        return NotFound();
    }

    public async Task<IActionResult> Delete(Guid id)
    {
        if (HttpContext.Request.Cookies["token"] is null)
        {
            return Redirect("https://localhost:7186/auth/login");
        }
        try
        {
            await _crudService.DeleteAsync($"/Brands/Remove/{id}", id);
        }
        catch (ApiException ex)
        {
            if (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                ViewBag.StatusCode = ex.StatusCode;
                ViewBag.ErrorMessage = ex.ModelErrors[""];

                return View("Error");
            }
        }
        catch (HttpRequestException ex)
        {
            return Redirect("https://localhost:7186/auth/login");
        }
        catch (Exception ex)
        {

            return View("Error");
        }

        return RedirectToAction(nameof(Index));
    }
}
