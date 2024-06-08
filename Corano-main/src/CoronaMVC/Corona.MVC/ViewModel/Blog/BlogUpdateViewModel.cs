namespace Corona.MVC.ViewModel.Blog;

public class BlogUpdateViewModel
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public ICollection<IFormFile>? UpdateBlogImagesDto { get; set; }
}
