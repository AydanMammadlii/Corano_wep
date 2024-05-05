using Corona.Application.Abstraction.Services;
using Corona.Application.DTOs.Brands;
using Corona.Application.DTOs.Categorys;
using Corona.Persistance.Implementations.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Corona.API.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class CategorysController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategorysController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var category = await _categoryService.GetAllAsync();
        return Ok(category);
    }

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var category = await _categoryService.GetByIdAsync(id);
        return Ok(category);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCategory([FromForm] CreateCategoryDto createCategoryDto)
    {
        await _categoryService.CreateAsync(createCategoryDto);
        return StatusCode((int)HttpStatusCode.Created);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateCategory([FromForm] UpdateCategoryDto updateCategoryDto)
    {
        await _categoryService.UpdateAsync(updateCategoryDto);
        return Ok();
    }

    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> Remove(Guid id)
    {
        await _categoryService.RemoveAsync(id);
        return Ok();
    }
}
