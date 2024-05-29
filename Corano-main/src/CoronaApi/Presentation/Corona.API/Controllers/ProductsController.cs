using Corona.Application.Abstraction.Services;
using Corona.Application.DTOs.Products;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Corona.API.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] string? Category,
        [FromQuery] List<string>? Brand,
        [FromQuery] List<string>? Color,
        [FromQuery] List<string>? Size,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 15,
        [FromQuery] int? minPrice = null,
        [FromQuery] int? maxPrice = null)
    {
        var products = await _productService.GetAllAsync(Category, Brand, Color, Size, pageNumber, pageSize, minPrice, maxPrice);
        return Ok(products);
    }

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var product = await _productService.GetByIdAsync(id);
        return Ok(product);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromForm] CreateProductDto createProductDto)
    {
        await _productService.CreateAsync(createProductDto);
        return StatusCode((int)HttpStatusCode.Created);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateProduct([FromForm] UpdateProductDto updateProductDto)
    {
        await _productService.UpdateAsync(updateProductDto);
        return Ok();
    }

    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> Remove(Guid id)
    {
        await _productService.RemoveAsync(id);
        return Ok();
    }
}