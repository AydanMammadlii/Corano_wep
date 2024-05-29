using Corona.Application.Abstraction.Services;
using Corona.Application.DTOs.Slider;
using Corona.Application.DTOs.Testimonial;
using Corona.Persistance.Implementations.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Corona.API.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class TestimonialsController : ControllerBase
{
    private readonly ITestimonialService _testimonialService;
    public TestimonialsController(ITestimonialService testimonialService)
        => _testimonialService = testimonialService;


    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var testimonial = await _testimonialService.GetAllAsync();
        return Ok(testimonial);
    }

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var testimonial = await _testimonialService.GetByIdAsync(id);
        return Ok(testimonial);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTestimonial([FromForm] TestimonialsCreateDto testimonialsCreateDto)
    {
        await _testimonialService.CreateAsync(testimonialsCreateDto);
        return StatusCode((int)HttpStatusCode.Created);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateTestimonial([FromForm] TestimonialsUpdateDto testimonialsUpdateDto)
    {
        await _testimonialService.UpdateAsync(testimonialsUpdateDto);
        return Ok();
    }

    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> Remove(Guid id)
    {
        await _testimonialService.RemoveAsync(id);
        return Ok();
    }
}