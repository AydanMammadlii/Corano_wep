using Corona.Application.DTOs.Brands;
using Corona.Application.DTOs.Products;

namespace Corona.Application.Abstraction.Services;

public interface IProductService
{
    Task<List<GetProductDto>> GetAllAsync(string? Category, List<string>? Brand, List<string>? Color, List<string>? Size, int pageNumber = 1, int pageSize = 15, int? minPrice = null, int? maxPrice = null);
    Task CreateAsync(CreateProductDto createProductDto);
    Task<GetProductDto> GetByIdAsync(Guid Id);
    Task UpdateAsync(UpdateProductDto updateProductDto);
    Task RemoveAsync(Guid Id);
}
