using Microsoft.AspNetCore.Http;

namespace Corona.Application.DTOs.Testimonial;

public class TestimonialsCreateDto
{
    public IFormFile ImageUrl { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}