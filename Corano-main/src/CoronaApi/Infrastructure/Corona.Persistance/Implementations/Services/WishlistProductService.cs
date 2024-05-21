using Corona.Application.Abstraction.Repositories.IEntityRepository;
using Corona.Application.Abstraction.Services;
using Corona.Application.DTOs.WishlistDto;
using Corona.Domain.Entities;
using Corona.Persistance.Context;
using Microsoft.EntityFrameworkCore;

namespace Corona.Persistance.Implementations.Services;

public class WishlistProductService : IWishlistProductService
{
    private readonly IWishlistProductReadRepository _readRepository;
    private readonly IWishlistProductWriteRepository _writeRepository;
    private readonly AppDbContext _appDbContext;
    public WishlistProductService(IWishlistProductReadRepository readRepository, IWishlistProductWriteRepository writeRepository, AppDbContext appDbContext)
    {
        _readRepository = readRepository;
        _writeRepository = writeRepository;
        _appDbContext = appDbContext;
    }
    public async Task CreateAsync(CreateWishlistProductDto createWishlistProductDto)
    {
        var wishlist = await _appDbContext.Wishlists.FirstOrDefaultAsync(x => x.AppUserId == createWishlistProductDto.AppUserId);
        if (wishlist is null) throw new Exception("Exception");
        var newWishlistProduct = new WishlistProduct()
        {
            ProductId = createWishlistProductDto.ProductId,
            WishlistId = wishlist.Id
        };

        await _writeRepository.AddAsync(newWishlistProduct);
        await _writeRepository.SaveChangeAsync();
    }

    public async Task<List<GetWishlistProductDto>> GetAllAsync(string AppUserId)
    {
        var wishlistProduct = await _appDbContext.WishlistProducts
                                    .Include(x => x.Product)
                                    .ThenInclude(x => x.ProductsImages)
                                    .Include(x => x.Wishlist)
                                    .Where(x => x.Wishlist.AppUserId == AppUserId)
                                    .ToListAsync();


        var listWishlistProduct = new List<GetWishlistProductDto>();
        foreach (var item in wishlistProduct)
        {
            var wishlistproduct = new GetWishlistProductDto()
            {
                ProductId = item.ProductId,
                WishlistId = item.WishlistId,
                Price = item.Product.Price,
                ImageUrl = item.Product.ProductsImages.First().ImageUrl,
                ProductTitle = item.Product.Title
            };

            listWishlistProduct.Add(wishlistproduct);
        }

        return listWishlistProduct;
    }

    public async Task RemoveAsync(RemoveWishlistProductDto removeWishlistProductDto)
    {
        var wishlist = await _appDbContext.Wishlists.FirstOrDefaultAsync(x => x.AppUserId == removeWishlistProductDto.AppUserId);
        if (wishlist is null) throw new Exception("Exception");

        var wishlistprodut = await _appDbContext.WishlistProducts.FirstOrDefaultAsync(x => x.WishlistId == wishlist.Id && x.ProductId == removeWishlistProductDto.ProductId);
        if (wishlistprodut is null) throw new Exception("Exception");

        _writeRepository.Remove(wishlistprodut);
        await _writeRepository.SaveChangeAsync();
    }
}