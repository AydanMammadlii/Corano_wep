using Corona.Application.Abstraction.Services;
using Corona.Application.DTOs.Blogs;
using Corona.Application.DTOs.Testimonial;
using Corona.Persistance.Implementations.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Corona.API.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class BlogsController : ControllerBase
{
    private readonly IBlogService _blogService;

    public BlogsController(IBlogService blogService)
    {
        _blogService = blogService;
    }


    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var blogs = await _blogService.GetAllAsync();
        return Ok(blogs);
    }

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var blog = await _blogService.GetByIdAsync(id);
        return Ok(blog);
    }

    [HttpPost]
    public async Task<IActionResult> CreateBlog([FromForm] CreateBlogDto createBlogDto)
    {
        await _blogService.CreateAsync(createBlogDto);
        return StatusCode((int)HttpStatusCode.Created);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateBlog([FromForm] UpdateBlogDto updateBlogDto)
    {
        await _blogService.UpdateAsync(updateBlogDto);
        return Ok();
    }

    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> Remove(Guid id)
    {
        await _blogService.RemoveAsync(id);
        return Ok();
    }
}