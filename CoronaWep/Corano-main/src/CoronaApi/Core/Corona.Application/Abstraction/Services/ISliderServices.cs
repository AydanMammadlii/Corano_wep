using Corona.Application.DTOs.Slider;
using Corona.Domain.Entities;

namespace Corona.Application.Abstraction.Services;

public interface ISliderServices
{
    Task<List<SliderGetDTO>> GetAllAsync();
    Task CreateAsync(SliderCreateDTO sliderCreateDTO);
    Task<SliderGetDTO> GetByIdAsync(Guid Id);
    Task UpdateAsync(SliderUpdateDTO sliderUpdateDTO);
    Task RemoveAsync(Guid Id);
}
