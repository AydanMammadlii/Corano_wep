using Corona.Application.DTOs.Categorys;

namespace Corona.MVC.ViewModel.Brand;

public class BrandViewModel
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public ICollection<GetCategoryDto> GetCategoryDtos { get; set; }
}
