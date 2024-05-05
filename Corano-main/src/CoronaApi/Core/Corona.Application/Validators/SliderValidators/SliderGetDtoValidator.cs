using FluentValidation;
using Corona.Application.DTOs.Slider;

namespace Corona.Application.Validators.SliderValidators;

public class SliderGetDtoValidator : AbstractValidator<SliderGetDTO>
{
    public SliderGetDtoValidator()
    {
        RuleFor(x => x.Image).NotNull().NotEmpty().MaximumLength(12000);
    }
}
