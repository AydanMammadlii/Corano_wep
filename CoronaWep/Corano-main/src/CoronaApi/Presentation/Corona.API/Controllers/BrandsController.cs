using Corona.Application.Abstraction.Services;
using Corona.Application.DTOs.Brands;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Corona.API.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class BrandsController : ControllerBase
{
    private readonly IBrandService _brandService;
    public BrandsController(IBrandService brandService)
    {
        _brandService = brandService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var brand = await _brandService.GetAllAsync();
        return Ok(brand);
    }

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var brand = await _brandService.GetByIdAsync(id);
        return Ok(brand);
    }

    [HttpPost]
    public async Task<IActionResult> CreateBrand([FromForm] CreateBrandDto createBrandDto)
    {
        await _brandService.CreateAsync(createBrandDto);
        return StatusCode((int)HttpStatusCode.Created);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateBrand([FromForm] UpdateBrandDto updateBrandDto)
    {
        await _brandService.UpdateAsync(updateBrandDto);
        return Ok();
    }

    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> Remove(Guid id)
    {
        await _brandService.RemoveAsync(id);
        return Ok();
    }

}
