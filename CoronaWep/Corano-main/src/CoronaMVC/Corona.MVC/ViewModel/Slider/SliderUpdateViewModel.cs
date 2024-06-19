namespace Corona.MVC.ViewModel.Slider;

public class SliderUpdateViewModel
{
    public Guid Id { get; set; }
    public IFormFile? Image { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
}
