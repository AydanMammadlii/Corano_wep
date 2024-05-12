using Corona.Application.DTOs.Brands;
using Corona.Application.DTOs.Slider;

namespace Corona.Application.Abstraction.Services;

public interface IBrandService
{
    Task<List<GetBrandDto>> GetAllAsync();
    Task CreateAsync(CreateBrandDto createBrandDto);
    Task<GetBrandDto> GetByIdAsync(Guid Id);
    Task UpdateAsync(UpdateBrandDto updateBrandDto);
    Task RemoveAsync(Guid Id);
}