using AutoMapper;
using Corona.Application.Abstraction.Repositories.IEntityRepository;
using Corona.Application.Abstraction.Services;
using Corona.Application.DTOs.ProductTypes;
using Corona.Domain.Entities;
using Corona.Persistance.Exceptions;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Corona.Persistance.Implementations.Services;

public class ProductTypeService : IProductTypeService
{
    private readonly IMapper _mapper;
    private readonly IProductTypeReadRepository _readRepository;
    private readonly IProductTypeWriteRepository _writeRepository;
    private readonly IConfiguration _configuration;

    public ProductTypeService(IMapper mapper, IProductTypeReadRepository readRepository, IProductTypeWriteRepository writeRepository, IConfiguration configuration)
    {
        _mapper = mapper;
        _readRepository = readRepository;
        _writeRepository = writeRepository;
        _configuration = configuration;
    }

    public async Task CreateAsync(CreateProductTypeDto createProductTypeDto)
    {
        if (createProductTypeDto.ImageUrl == null || createProductTypeDto.ImageUrl.Length == 0)
            throw new Exception("Exception");

        string ApiKey = _configuration["GoogleCloud:ApiKey"];
        var credential = GoogleCredential.FromFile(ApiKey);

        var client = StorageClient.Create(credential);
        var imageUrl = string.Empty;

        using (var memoryStream = new MemoryStream())
        {
            await createProductTypeDto.ImageUrl.CopyToAsync(memoryStream);
            memoryStream.Position = 0;

            var objectName = $"Test/{Guid.NewGuid()}_{createProductTypeDto.ImageUrl.FileName}";
            var bucketName = "testes22d";

            await client.UploadObjectAsync(bucketName, objectName, null, memoryStream);
            var url = $"https://storage.googleapis.com/{bucketName}/{objectName}";

            imageUrl = url;
        }
        var newProductType = _mapper.Map<ProductType>(createProductTypeDto);
        newProductType.ImageUrl = imageUrl;

        await _writeRepository.AddAsync(newProductType);
        await _writeRepository.SaveChangeAsync();
    }

    public async Task<List<GetProductTypeDto>> GetAllAsync()
    {
        var allProductType = await _readRepository.GetAll().ToListAsync();
        return _mapper.Map<List<GetProductTypeDto>>(allProductType);
    }

    public async Task<GetProductTypeDto> GetByIdAsync(Guid Id)
    {
        var productType = await _readRepository.GetByIdAsync(Id);
        if (productType is null) throw new NotFoundException("Not found product type");

        return _mapper.Map<GetProductTypeDto>(productType);
    }

    public async Task RemoveAsync(Guid Id)
    {
        var productType = await _readRepository.GetByIdAsync(Id);
        if (productType is null) throw new NotFoundException("Not found product type");

        _writeRepository.Remove(productType);
        await _writeRepository.SaveChangeAsync();
    }

    public async Task UpdateAsync(UpdateProductTypeDto updateProductTypeDto)
    {
        var productType = await _readRepository.GetByIdAsync(updateProductTypeDto.Id);
        if (productType is null) throw new NotFoundException("Not found product type");

        if (updateProductTypeDto.ImageUrl != null)
        {
            string ApiKey = _configuration["GoogleCloud:ApiKey"];
            var credential = GoogleCredential.FromFile(ApiKey);

            var client = StorageClient.Create(credential);
            var imageUrl = string.Empty;

            using (var memoryStream = new MemoryStream())
            {
                await updateProductTypeDto.ImageUrl.CopyToAsync(memoryStream);
                memoryStream.Position = 0;

                var objectName = $"Test/{Guid.NewGuid()}_{updateProductTypeDto.ImageUrl.FileName}";
                var bucketName = "testes22d";

                await client.UploadObjectAsync(bucketName, objectName, null, memoryStream);
                var url = $"https://storage.googleapis.com/{bucketName}/{objectName}";

                imageUrl = url;
            }
            productType.ImageUrl = imageUrl;
        }

        productType.ImageUrl = productType.ImageUrl;

        productType.Title = updateProductTypeDto.Title ?? productType.Title;
        productType.Description = updateProductTypeDto.Description ?? productType.Title;

        _writeRepository.Update(productType);
        await _writeRepository.SaveChangeAsync();
    }
}
