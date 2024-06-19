using Corona.Application.DTOs.BlogImages;

namespace Corona.Application.DTOs.Blogs;

public class GetBlogDto
{
    public Guid Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public ICollection<GetBlogImageDto>? GetBlogImageDtos { get; set; }
}
