using AutoMapper;
using Corona.Application.Abstraction.Repositories.IEntityRepository;
using Corona.Application.Abstraction.Services;
using Corona.Application.DTOs.BlogImages;
using Corona.Application.DTOs.Blogs;
using Corona.Application.DTOs.Products;
using Corona.Application.DTOs.ProductsImages;
using Corona.Domain.Entities;
using Corona.Persistance.Context;
using Corona.Persistance.Exceptions;
using Corona.Persistance.Implementations.Repositories.IEntityRepository;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Drawing;

namespace Corona.Persistance.Implementations.Services;

public class ProductService : IProductService
{
    private readonly IMapper _mapper;
    private readonly AppDbContext _appDbContext;
    private readonly IProductReadRepository _productReadRepository;
    private readonly IProductWriteRepository _productWriteRepository;
    private readonly IConfiguration _configuration;

    public ProductService(IMapper mapper, AppDbContext appDbContext, IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository, IConfiguration configuration)
    {
        _mapper = mapper;
        _appDbContext = appDbContext;
        _productReadRepository = productReadRepository;
        _productWriteRepository = productWriteRepository;
        _configuration = configuration;
    }

    public async Task CreateAsync(CreateProductDto createProductDto)
    {
        var productType = await _appDbContext.ProductTypes.FirstOrDefaultAsync(x => x.Id == createProductDto.ProductTypeId);
        if (productType is null) throw new NotFoundException("Product type not found");

        var category = await _appDbContext.Categories.FirstOrDefaultAsync(x => x.Id == createProductDto.CategoryId);
        if (category is null) throw new NotFoundException("Product type not found");

        var newProduct = new Product()
        {
            Title = createProductDto.Title,
            Description = createProductDto.Description,
            StockCount = createProductDto.StockCount,
            Size = createProductDto.Size,
            Color = createProductDto.Color,
            Price = createProductDto.Price,
            ProductTypeId = createProductDto.ProductTypeId,
            CategoryId = createProductDto.CategoryId
        };

        await _productWriteRepository.AddAsync(newProduct);
        await _productWriteRepository.SaveChangeAsync();

        var newProductImage = new List<CreateProductsImageDto>();

        if (createProductDto.ProductsImages is not null)
        {

            foreach (var item in createProductDto.ProductsImages)
            {
                string ApiKey = _configuration["GoogleCloud:ApiKey"];
                var credential = GoogleCredential.FromFile(ApiKey);

                var client = StorageClient.Create(credential);
                var imageUrl = string.Empty;

                using (var memoryStream = new MemoryStream())
                {
                    await item.CopyToAsync(memoryStream);
                    memoryStream.Position = 0;

                    var objectName = $"Test/{Guid.NewGuid()}_{item.FileName}";
                    var bucketName = "testes22d";

                    await client.UploadObjectAsync(bucketName, objectName, null, memoryStream);
                    var url = $"https://storage.googleapis.com/{bucketName}/{objectName}";

                    imageUrl = url;
                }
                var productImage = new CreateProductsImageDto()
                {
                    ProductId = newProduct.Id,
                    ImageUrl = imageUrl
                };
                newProductImage.Add(productImage);
            }
        }

        var productImages = _mapper.Map<List<ProductImage>>(newProductImage);
        await _appDbContext.ProductImages.AddRangeAsync(productImages);
        await _appDbContext.SaveChangesAsync();
    }

    public async Task<List<GetProductDto>> GetAllAsync(string? Category, List<string>? Brand, List<string>? Color, List<string>? Size, int pageNumber = 1, int pageSize = 15, int? minPrice = null, int? maxPrice = null)
    {
        var query = _productReadRepository.GetAll()
            .Include(x => x.ProductsImages)
            .Include(x => x.Category)
            .ThenInclude(x => x.Brand)
            .OrderByDescending(x => x.CreatedDate)
            .AsQueryable();

        if (!string.IsNullOrEmpty(Category))
        {
            query = query.Where(x => x.Category.Title.Contains(Category));
        }

        if (Brand != null && Brand.Any())
        {
            query = query.Where(x => Brand.Contains(x.Category.Brand.Title));
        }

        if (Color != null && Color.Any())
        {
            query = query.Where(x => Color.Contains(x.Color));
        }

        if (Size != null && Size.Any())
        {
            query = query.Where(x => Size.Contains(x.Size));
        }

        if (minPrice.HasValue)
        {
            query = query.Where(x => x.Price >= minPrice.Value);
        }

        if (maxPrice.HasValue)
        {
            query = query.Where(x => x.Price <= maxPrice.Value);
        }

        // Calculate the number of records to skip
        int skip = (pageNumber - 1) * pageSize;

        // Apply pagination
        query = query.Skip(skip).Take(pageSize);

        var allProductImage = await query.ToListAsync();
        return _mapper.Map<List<GetProductDto>>(allProductImage);
    }




    public async Task<GetProductDto> GetByIdAsync(Guid Id)
    {
        var product = await _appDbContext.Products.Include(x => x.ProductsImages)
                            .FirstOrDefaultAsync(x => x.Id == Id);
        if (product is null) throw new NotFoundException("not found");

        return _mapper.Map<GetProductDto>(product);
    }

    public async Task RemoveAsync(Guid Id)
    {
        var product = await _productReadRepository.GetByIdAsync(Id);
        if (product is null) throw new NotFoundException("not found");

        _productWriteRepository.Remove(product);
        await _productWriteRepository.SaveChangeAsync();
    }

    public async Task UpdateAsync(UpdateProductDto updateProductDto)
    {
        var product = await _appDbContext.Products.Include(x => x.ProductsImages)
                       .FirstOrDefaultAsync(x => x.Id == updateProductDto.Id);
        if (product is null) throw new NotFoundException("Blog not found");

        var productType = await _appDbContext.ProductTypes.FirstOrDefaultAsync(x => x.Id == updateProductDto.ProductTypeId);
        if (productType is null) throw new NotFoundException("Product type not found");

        var category = await _appDbContext.Categories.FirstOrDefaultAsync(x => x.Id == updateProductDto.CategoryId);
        if (category is null) throw new NotFoundException("Product type not found");


        product.Title = updateProductDto.Title;
        product.Description = updateProductDto.Description;
        product.StockCount = updateProductDto.StockCount;
        product.Size = updateProductDto.Size;
        product.Color = updateProductDto.Color;
        product.Price = updateProductDto.Price;
        product.ProductTypeId = updateProductDto.ProductTypeId;
        product.CategoryId = updateProductDto.CategoryId;

        _appDbContext.ProductImages.RemoveRange(product.ProductsImages);
        await _appDbContext.SaveChangesAsync();

        var newProductImage = new List<CreateProductsImageDto>();

        if (updateProductDto.UpdateProductsImages is not null)
        {

            foreach (var item in updateProductDto.UpdateProductsImages)
            {
                string ApiKey = _configuration["GoogleCloud:ApiKey"];
                var credential = GoogleCredential.FromFile(ApiKey);

                var client = StorageClient.Create(credential);
                var imageUrl = string.Empty;

                using (var memoryStream = new MemoryStream())
                {
                    await item.CopyToAsync(memoryStream);
                    memoryStream.Position = 0;

                    var objectName = $"Test/{Guid.NewGuid()}_{item.FileName}";
                    var bucketName = "testes22d";

                    await client.UploadObjectAsync(bucketName, objectName, null, memoryStream);
                    var url = $"https://storage.googleapis.com/{bucketName}/{objectName}";

                    imageUrl = url;
                }

                var productImage = new CreateProductsImageDto()
                {
                    ProductId = product.Id,
                    ImageUrl = imageUrl
                };
                newProductImage.Add(productImage);
            }
        }

        var productImages = _mapper.Map<List<ProductImage>>(newProductImage);
        await _appDbContext.ProductImages.AddRangeAsync(productImages);
        await _appDbContext.SaveChangesAsync();

        _productWriteRepository.Update(product);
        await _productWriteRepository.SaveChangeAsync();
    }
}