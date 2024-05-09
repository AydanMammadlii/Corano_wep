namespace Corona.Application.DTOs.Categorys;

public class UpdateCategoryDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public Guid BrandId { get; set; }
}