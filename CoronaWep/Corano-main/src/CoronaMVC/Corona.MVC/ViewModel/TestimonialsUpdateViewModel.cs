namespace Corona.MVC.ViewModel;

public class TestimonialsUpdateViewModel
{
    public Guid Id { get; set; }
    public IFormFile ImageUrl { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}
