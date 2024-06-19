using Corona.Application.DTOs.Brands;
using Corona.Application.DTOs.ProductTypes;

namespace Corona.Application.Abstraction.Services;

public interface IProductTypeService
{
    Task<List<GetProductTypeDto>> GetAllAsync();
    Task CreateAsync(CreateProductTypeDto createProductTypeDto);
    Task<GetProductTypeDto> GetByIdAsync(Guid Id);
    Task UpdateAsync(UpdateProductTypeDto updateProductTypeDto);
    Task RemoveAsync(Guid Id);
}
