using Corona.Application.DTOs.Brands;
using Corona.Application.DTOs.Categorys;

namespace Corona.Application.Abstraction.Services;

public interface ICategoryService
{
    Task<List<GetCategoryDto>> GetAllAsync();
    Task CreateAsync(CreateCategoryDto createCategoryDto);
    Task<GetCategoryDto> GetByIdAsync(Guid Id);
    Task UpdateAsync(UpdateCategoryDto updateCategoryDto);
    Task RemoveAsync(Guid Id);
}
