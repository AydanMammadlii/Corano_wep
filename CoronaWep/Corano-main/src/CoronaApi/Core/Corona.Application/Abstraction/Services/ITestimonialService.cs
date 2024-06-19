using Corona.Application.DTOs.Slider;
using Corona.Application.DTOs.Testimonial;

namespace Corona.Application.Abstraction.Services;

public interface ITestimonialService
{
    Task<List<TestimonialsGetDto>> GetAllAsync();
    Task CreateAsync(TestimonialsCreateDto testimonialsCreateDto);
    Task<TestimonialsGetDto> GetByIdAsync(Guid Id);
    Task UpdateAsync(TestimonialsUpdateDto testimonialsUpdateDto);
    Task RemoveAsync(Guid Id);
}
