using Microsoft.AspNetCore.Http;

namespace Corona.Application.DTOs.ProductTypes;

public class CreateProductTypeDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public IFormFile ImageUrl { get; set; }
}
