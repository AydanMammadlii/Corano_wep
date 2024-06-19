using Microsoft.AspNetCore.Http;

namespace Corona.Application.DTOs.Slider;

public class SliderCreateDTO
{
    public IFormFile Image { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}
