using Corona.Application.DTOs.BasketProducts;
using Corona.Application.DTOs.WishlistDto;

namespace Corona.Application.Abstraction.Services;

public interface IWishlistProductService
{
    Task<List<GetWishlistProductDto>> GetAllAsync(string AppUserId);
    Task CreateAsync(CreateWishlistProductDto createWishlistProductDto);
    Task RemoveAsync(RemoveWishlistProductDto removeWishlistProductDto);
}