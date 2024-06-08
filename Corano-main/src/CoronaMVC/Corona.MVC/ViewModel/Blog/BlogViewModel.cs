namespace Corona.MVC.ViewModel.Blog;

public class BlogViewModel
{
    public Guid Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public ICollection<BlogImageViewModel>? GetBlogImageDtos { get; set; }
    public List<IFormFile> UpdateBlogImagesDto { get; set; }  
}
