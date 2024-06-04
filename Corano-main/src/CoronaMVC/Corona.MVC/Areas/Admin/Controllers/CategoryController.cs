﻿using Corona.MVC.ExamProgramUIExceptions;
using Corona.MVC.Service.Interfaces;
using Corona.MVC.ViewModel.Brand;
using Corona.MVC.ViewModel.Category;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Corona.MVC.Areas.Admin.Controllers;

[Area("Admin")]
public class CategoryController : Controller
{
    private readonly ICrudService _crudService;
    Uri baseAddress = new Uri("https://localhost:7295/api");
    private readonly HttpClient _httpClient;

    public CategoryController(ICrudService crudService, HttpClient httpClient)
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
        var datas = await _crudService.GetAllAsync<IEnumerable<CategoryViewModel>>("/Categorys/GetAll");

        return View(datas);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        if (HttpContext.Request.Cookies["token"] is null)
        {
            return Redirect("https://localhost:7186/auth/login");
        }
        var datas = await _crudService.GetAllAsync<IEnumerable<BrandViewModel>>("/Brands/GetAll");
        ViewBag.brands = datas;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(BrandCreateViewModel model)
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
        if (HttpContext.Request.Cookies["token"] is null)
        {
            return Redirect("https://localhost:7186/auth/login");
        }
        var datas = await _crudService.GetAllAsync<IEnumerable<BrandViewModel>>("/Brands/GetAll");
        ViewBag.brands = datas;

        var response = await _httpClient.GetAsync($"{_httpClient.BaseAddress}/Categorys/GetById/{id}");
        if (response.IsSuccessStatusCode)
        {
            var stringData = await response.Content.ReadAsStringAsync();
            var category = JsonConvert.DeserializeObject<CategoryViewModel>(stringData);
            return View(category);
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
