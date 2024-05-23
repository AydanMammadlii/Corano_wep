using Microsoft.AspNetCore.Http;

namespace Corona.Application.DTOs.Blogs;

public class CreateBlogDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public ICollection<IFormFile>? CreateBlogImagesDto { get; set; }
}