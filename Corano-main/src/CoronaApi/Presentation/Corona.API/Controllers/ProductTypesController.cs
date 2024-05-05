using Corona.Application.Abstraction.Services;
using Corona.Application.DTOs.ProductTypes;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Corona.API.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class ProductTypesController : ControllerBase
{
    private readonly IProductTypeService _productTypeService;

    public ProductTypesController(IProductTypeService productTypeService)
    {
        _productTypeService = productTypeService;
    }


    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var productType = await _productTypeService.GetAllAsync();
        return Ok(productType);
    }

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var productType = await _productTypeService.GetByIdAsync(id);
        return Ok(productType);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProductType([FromForm] CreateProductTypeDto createProductTypeDto)
    {
        await _productTypeService.CreateAsync(createProductTypeDto);
        return StatusCode((int)HttpStatusCode.Created);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateProductType([FromForm] UpdateProductTypeDto updateProductTypeDto)
    {
        await _productTypeService.UpdateAsync(updateProductTypeDto);
        return Ok();
    }

    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> Remove(Guid id)
    {
        await _productTypeService.RemoveAsync(id);
        return Ok();
    }

}
