using Corona.Application.DTOs.Slider;

namespace Corona.Application.Abstraction.Services;

public interface ISliderServices
{
    Task<List<SliderGetDTO>> GetAllAsync();
    Task CreateAsync(SliderCreateDTO sliderCreateDTO);
    Task<SliderGetDTO> GetByIdAsync(Guid Id);
    Task UpdateAsync(SliderUpdateDTO sliderUpdateDTO);
    Task RemoveAsync(Guid Id);
}