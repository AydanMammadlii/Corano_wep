namespace Corona.Application.DTOs.Categorys;

public class GetCategoryDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public Guid BrandId { get; set; }
    public string BrandName { get; set; }
}
