using AutoMapper;
using Corona.Application.Abstraction.Repositories.IEntityRepository;
using Corona.Application.Abstraction.Services;
using Corona.Application.DTOs.Categorys;
using Corona.Domain.Entities;
using Corona.Persistance.Context;
using Corona.Persistance.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Corona.Persistance.Implementations.Services;

public class CategoryService : ICategoryService
{
    private readonly IMapper _mapper;
    private readonly ICategoryReadRepository _readCategoryRepository;
    private readonly ICategoryWriteRepository _writeCategoryRepository;
    private readonly IBrandReadRepository _brandReadRepository;
    private readonly AppDbContext _appDbContext;

    public CategoryService(IMapper mapper, ICategoryReadRepository readCategoryRepository, ICategoryWriteRepository writeCategoryRepository, IBrandReadRepository brandReadRepository, AppDbContext appDbContext)
    {
        _mapper = mapper;
        _readCategoryRepository = readCategoryRepository;
        _writeCategoryRepository = writeCategoryRepository;
        _brandReadRepository = brandReadRepository;
        _appDbContext = appDbContext;
    }

    public async Task CreateAsync(CreateCategoryDto createCategoryDto)
    {
        var brand = await _brandReadRepository.GetByIdAsync(createCategoryDto.BrandId);
        if (brand is null) throw new NotFoundException("Not found brand");

        var newCategory = _mapper.Map<Category>(createCategoryDto);

        await _writeCategoryRepository.AddAsync(newCategory);
        await _writeCategoryRepository.SaveChangeAsync();
    }

    public async Task<List<GetCategoryDto>> GetAllAsync()
    {
        var category = await _readCategoryRepository.GetAll().Include(x=>x.Brand).ToListAsync();
        var toMapper = _mapper.Map<List<GetCategoryDto>>(category);
        toMapper.ForEach(x =>
        {
            var brand = category.FirstOrDefault(c => c.Id == x.Id)?.Brand;
            if (brand != null)
            {
                x.BrandName = brand.Title;
            }
        });
        return toMapper;
    }

    public async Task<GetCategoryDto> GetByIdAsync(Guid Id)
    {
        var category = await _appDbContext.Categories.Include(x=>x.Brand).FirstOrDefaultAsync(x=>x.Id==Id);
        if (category is null) throw new NotFoundException("Not found category");

        var toDto = _mapper.Map<GetCategoryDto>(category);
        toDto.BrandName = category.Brand.Title;
        return toDto;
    }

    public async Task RemoveAsync(Guid Id)
    {
        var category = await _readCategoryRepository.GetByIdAsync(Id);
        if (category is null) throw new NotFoundException("Not found category");

        _writeCategoryRepository.Remove(category);
        await _writeCategoryRepository.SaveChangeAsync();
    }

    public async Task UpdateAsync(UpdateCategoryDto updateCategoryDto)
    {
        var brand = await _brandReadRepository.GetByIdAsync(updateCategoryDto.BrandId);
        if (brand is null) throw new NotFoundException("Not found brand");

        var category = await _readCategoryRepository.GetByIdAsync(updateCategoryDto.Id);
        if (category is null) throw new NotFoundException("Not found category");

        _mapper.Map(updateCategoryDto, category);
        _writeCategoryRepository.Update(category);
        await _writeCategoryRepository.SaveChangeAsync();
    }
}
