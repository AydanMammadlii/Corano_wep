using AutoMapper;
using Corona.Application.Abstraction.Repositories.IEntityRepository;
using Corona.Application.Abstraction.Services;
using Corona.Application.DTOs.BasketProducts;
using Corona.Domain.Entities;
using Corona.Persistance.Context;
using Corona.Persistance.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Corona.Persistance.Implementations.Services;

public class BasketProductService : IBasketProductService
{
    private readonly IMapper _mapper;
    private readonly IBasketProductReadRepository _readRepository;
    private readonly IBasketProductWriteRepository _writeRepository;
    private readonly AppDbContext _appDbContext;

    public BasketProductService(IMapper mapper, IBasketProductReadRepository readRepository, IBasketProductWriteRepository writeRepository, AppDbContext appDbContext)
    {
        _mapper = mapper;
        _readRepository = readRepository;
        _writeRepository = writeRepository;
        _appDbContext = appDbContext;
    }

    public async Task CreateAsync(CreateBasketProductDto createBasketProductDto)
    {
        var product = await _appDbContext.Products.FirstOrDefaultAsync(x => x.Id == createBasketProductDto.ProductId);
        if (product.StockCount < createBasketProductDto.Count) throw new Exception("Stock count'dan az olmalidir");

        var basetproduct = _mapper.Map<BasketProduct>(createBasketProductDto);

        await _writeRepository.AddAsync(basetproduct);
        await _writeRepository.SaveChangeAsync();
    }

    public async Task<BasetTotalResultDto> GetAllAsync(string AppUserId)
    {
        var user = await _appDbContext.AppUsers
                                      .Include(x => x.Basket)
                                      .FirstOrDefaultAsync(x => x.Id == AppUserId);
        if (user is null) throw new NotFoundException("Not Found User");

        var youAllBasketproduct = await _readRepository.GetAll()
                                                       .Include(x => x.Product)
                                                       .ThenInclude(x => x.ProductsImages)
                                                       .Where(x => x.BasketId == user.Basket.Id)
                                                       .ToListAsync();

        var toDto = youAllBasketproduct.Select(bp => new GetBasketProductDto
        {
            ProductId = bp.ProductId,
            BasketId = bp.BasketId,
            Count = bp.Count,
            Price = (bp.Count * bp.Product.Price),
            ImageUrl = bp.Product.ProductsImages.First().ImageUrl,
            ProductTitle = bp.Product.Title
        }).ToList();


        var result = new BasetTotalResultDto()
        {
            GetBasketProductDto = toDto,
            TotalPrice = youAllBasketproduct.Sum(x => x.Product.Price * x.Count)
        };
        return result;
    }



    public async Task RemoveAsync(Guid BasketId)
    {
        var basket = await _appDbContext.Baskets.FirstOrDefaultAsync(x => x.Id == BasketId);
        if (basket is null) throw new NotFoundException("Basket not found");

        var basketProduct = await _readRepository.GetAll().Where(x => x.BasketId == BasketId).ToListAsync();
        if (basketProduct is not null)
        {
            _writeRepository.RemoveRange(basketProduct);
            await _writeRepository.SaveChangeAsync();
        }
    }

    public async Task UpdateAsync(UpdateBasketProductDto updateBasketProductDto)
    {
        var basketProduct = await _appDbContext.BasketProducts
            .FirstOrDefaultAsync(x => x.BasketId == updateBasketProductDto.BasketId &&
            x.ProductId == updateBasketProductDto.ProductId);

        if (basketProduct is null) throw new NotFoundException("not found");

        var product = await _appDbContext.Products.FirstOrDefaultAsync(x => x.Id == updateBasketProductDto.ProductId);
        if (product.StockCount < updateBasketProductDto.Count) throw new Exception("Stock count'dan az olmalidir");

        _mapper.Map(updateBasketProductDto, basketProduct);

        if (updateBasketProductDto.Count == 0)
        {
            _writeRepository.Remove(basketProduct);
            await _writeRepository.SaveChangeAsync();
        }
        else
        {
            _writeRepository.Update(basketProduct);
            await _writeRepository.SaveChangeAsync();
        }
    }
}