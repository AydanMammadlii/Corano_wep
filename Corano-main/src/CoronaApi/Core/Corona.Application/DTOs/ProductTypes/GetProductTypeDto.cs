namespace Corona.Application.DTOs.ProductTypes;

public class GetProductTypeDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
}