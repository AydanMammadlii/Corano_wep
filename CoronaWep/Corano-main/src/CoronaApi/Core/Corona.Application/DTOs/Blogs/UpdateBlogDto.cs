using Microsoft.AspNetCore.Http;

namespace Corona.Application.DTOs.Blogs;

public class UpdateBlogDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public ICollection<IFormFile>? UpdateBlogImagesDto { get; set; }
}
