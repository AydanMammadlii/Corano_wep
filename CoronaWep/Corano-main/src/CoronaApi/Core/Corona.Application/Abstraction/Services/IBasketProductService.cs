using Corona.Application.DTOs.BasketProducts;

namespace Corona.Application.Abstraction.Services;

public interface IBasketProductService
{
    Task<BasetTotalResultDto> GetAllAsync(string AppUserId);
    Task CreateAsync(CreateBasketProductDto createBasketProductDto);
    Task UpdateAsync(UpdateBasketProductDto updateBasketProductDto);
    Task RemoveAsync(Guid BasketId);
}
