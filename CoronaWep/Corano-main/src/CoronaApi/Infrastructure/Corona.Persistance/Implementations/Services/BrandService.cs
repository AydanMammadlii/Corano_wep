using AutoMapper;
using Corona.Application.Abstraction.Repositories.IEntityRepository;
using Corona.Application.Abstraction.Services;
using Corona.Application.DTOs.Brands;
using Corona.Domain.Entities;
using Corona.Persistance.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Corona.Persistance.Implementations.Services;

public class BrandService : IBrandService
{
    private readonly IMapper _mapper;
    private readonly IBrandReadRepository _brandReadRepository;
    private readonly IBrandWriteRepository _brandWriteRepository;

    public BrandService(IMapper mapper,
                        IBrandReadRepository brandReadRepository,
                        IBrandWriteRepository brandWriteRepository)
    {
        _mapper = mapper;
        _brandReadRepository = brandReadRepository;
        _brandWriteRepository = brandWriteRepository;
    }

    public async Task CreateAsync(CreateBrandDto createBrandDto)
    {
        var newBrand = _mapper.Map<Brand>(createBrandDto);

        await _brandWriteRepository.AddAsync(newBrand);
        await _brandWriteRepository.SaveChangeAsync();
    }

    public async Task<List<GetBrandDto>> GetAllAsync()
    {
        var allBrands = await _brandReadRepository.GetAll().Include(x=>x.Categories).ToListAsync();

        return _mapper.Map<List<GetBrandDto>>(allBrands);
    }

    public async Task<GetBrandDto> GetByIdAsync(Guid Id)
    {
        var brand = await _brandReadRepository.GetByIdAsync(Id);
        if (brand is null) throw new NotFoundException("brand not found");

        return _mapper.Map<GetBrandDto>(brand);
    }

    public async Task RemoveAsync(Guid Id)
    {
        var brand = await _brandReadRepository.GetByIdAsync(Id);
        if (brand is null) throw new NotFoundException("brand not found");

        _brandWriteRepository.Remove(brand);
        await _brandWriteRepository.SaveChangeAsync();
    }

    public async Task UpdateAsync(UpdateBrandDto updateBrandDto)
    {
        var brand = await _brandReadRepository.GetByIdAsync(updateBrandDto.Id);
        if (brand is null) throw new NotFoundException("brand not found");

        _mapper.Map(updateBrandDto, brand);
        _brandWriteRepository.Update(brand);
        await _brandWriteRepository.SaveChangeAsync();
    }
}
