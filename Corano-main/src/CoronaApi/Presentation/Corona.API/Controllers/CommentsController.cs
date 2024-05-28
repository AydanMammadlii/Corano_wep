using Corona.Application.Abstraction.Services;
using Corona.Application.DTOs.Brands;
using Corona.Application.DTOs.Comments;
using Corona.Persistance.Implementations.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Corona.API.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class CommentsController : ControllerBase
{
    private readonly ICommentService _commentService;

    public CommentsController(ICommentService commentService)
    {
        _commentService = commentService;
    }


    [HttpGet("{BlogId:Guid}")]
    public async Task<IActionResult> GetAll(Guid BlogId)
    {
        var comment = await _commentService.GetAllAsync(BlogId);
        return Ok(comment);
    }

    [HttpPost]
    public async Task<IActionResult> CreateComment([FromForm] CreateCommentDto createCommentDto)
    {
        await _commentService.CreateAsync(createCommentDto);
        return StatusCode((int)HttpStatusCode.Created);
    }

    [HttpDelete]
    public async Task<IActionResult> Remove([FromForm] RemoveCommentDto removeCommentDto)
    {
        await _commentService.RemoveAsync(removeCommentDto);
        return Ok();
    }
}