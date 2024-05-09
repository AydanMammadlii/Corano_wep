using Corona.Application.DTOs.Categorys;

namespace Corona.Application.DTOs.Brands;

public class GetBrandDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public ICollection<GetCategoryDto> GetCategoryDtos { get; set; }
}