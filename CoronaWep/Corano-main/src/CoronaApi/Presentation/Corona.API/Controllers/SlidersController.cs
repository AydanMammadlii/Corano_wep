using Corona.Application.Abstraction.Services;
using Corona.Application.DTOs.Slider;
using Corona.Persistance.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Corona.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SlidersController : ControllerBase
{
    private readonly ISliderServices _sliderService;
    private readonly AppDbContext _appDbContext;

    public SlidersController(ISliderServices sliderService, AppDbContext appDbContext)
    {
        _sliderService = sliderService;
        _appDbContext = appDbContext;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var slider = await _sliderService.GetAllAsync();
        return Ok(slider);
    }

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var bySlider = await _sliderService.GetByIdAsync(id);   
        return Ok(bySlider);
    }

    [HttpPost]
    public async Task<IActionResult> CreateSlider([FromForm] SliderCreateDTO sliderCreateDTO)
    {
        await _sliderService.CreateAsync(sliderCreateDTO);
        return StatusCode((int)HttpStatusCode.Created);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateSlider([FromForm] SliderUpdateDTO sliderUpdateDTO)
    {
        await _sliderService.UpdateAsync(sliderUpdateDTO);
        return Ok();    
    }

    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> Remove(Guid id)
    {
        await _sliderService.RemoveAsync(id);
        return Ok();
    }

}
