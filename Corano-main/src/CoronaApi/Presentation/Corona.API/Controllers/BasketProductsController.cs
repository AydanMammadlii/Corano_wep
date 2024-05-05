using Corona.Application.Abstraction.Services;
using Corona.Application.DTOs.BasketProducts;
using Corona.Domain.Entities;
using Corona.Persistance.Context;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Corona.API.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class BasketProductsController : ControllerBase
{
    private readonly IBasketProductService _productService;
    public BasketProductsController(IBasketProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(string AppUserId)
    {
        var basketProduct = await _productService.GetAllAsync(AppUserId);
        return Ok(basketProduct);
    }

    [HttpPost]
    public async Task<IActionResult> CreateBasketProduct([FromForm] CreateBasketProductDto createBasketProductDto)
    {
        await _productService.CreateAsync(createBasketProductDto);
        return StatusCode((int)HttpStatusCode.Created);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateBasketProduct([FromForm] UpdateBasketProductDto updateBasketProductDto)
    {
        await _productService.UpdateAsync(updateBasketProductDto);
        return Ok();
    }

    [HttpDelete("{BasketId:Guid}")]
    public async Task<IActionResult> Remove(Guid BasketId)
    {
        await _productService.RemoveAsync(BasketId);
        return Ok();
    }
}
