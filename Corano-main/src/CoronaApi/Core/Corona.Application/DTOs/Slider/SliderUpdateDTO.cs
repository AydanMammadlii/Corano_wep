using Microsoft.AspNetCore.Http;

namespace Corona.Application.DTOs.Slider;

public class SliderUpdateDTO
{
    public Guid Id { get; set; }
    public IFormFile? Image { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }

}