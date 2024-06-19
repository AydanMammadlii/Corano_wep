namespace Corona.MVC.ViewModel.Blog;

public class BlogCreateViewModel
{
    public string Title { get; set; }
    public string Description { get; set; }
    public ICollection<IFormFile>? CreateBlogImagesDto { get; set; }
}
