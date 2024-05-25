using Corona.Application.DTOs.Blogs;
using Corona.Application.DTOs.Slider;

namespace Corona.Application.Abstraction.Services;

public interface IBlogService
{
    Task CreateAsync(CreateBlogDto createBlogDto);
    Task<List<GetBlogDto>> GetAllAsync();
    Task<GetBlogDto> GetByIdAsync(Guid Id);
    Task UpdateAsync(UpdateBlogDto updateBlogDto);
    Task RemoveAsync(Guid Id);
}