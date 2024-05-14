using Microsoft.AspNetCore.Http;

namespace Corona.Application.DTOs.ProductTypes;

public class UpdateProductTypeDto
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public IFormFile? ImageUrl { get; set; }
}