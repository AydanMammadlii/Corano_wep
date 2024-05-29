using Corona.Application.Abstraction.Services;
using Corona.Application.DTOs.BasketProducts;
using Corona.Application.DTOs.WishlistDto;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Corona.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WishlistProductsController : ControllerBase
{
    private readonly IWishlistProductService _wishlistProductService;
    public WishlistProductsController(IWishlistProductService wishlistProductService)
    {
        _wishlistProductService = wishlistProductService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(string AppUserId)
    {
        var wishlistProduct = await _wishlistProductService.GetAllAsync(AppUserId);
        return Ok(wishlistProduct);
    }

    [HttpPost]
    public async Task<IActionResult> CreateBasketProduct([FromForm] CreateWishlistProductDto createWishlistProductDto)
    {
        await _wishlistProductService.CreateAsync(createWishlistProductDto);
        return StatusCode((int)HttpStatusCode.Created);
    }


    [HttpDelete]
    public async Task<IActionResult> Remove([FromForm] RemoveWishlistProductDto removeWishlistProductDto)
    {
        await _wishlistProductService.RemoveAsync(removeWishlistProductDto);
        return Ok();
    }
}