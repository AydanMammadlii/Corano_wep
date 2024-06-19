using Microsoft.AspNetCore.Http;

namespace Corona.Application.DTOs.Testimonial;

public class TestimonialsUpdateDto
{
    public Guid Id { get; set; }
    public IFormFile? ImageUrl { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}
