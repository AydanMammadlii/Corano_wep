using FluentValidation;
using Corona.Application.DTOs.Slider;

namespace Corona.Application.Validators.SliderValidators;

public class SliderUpdateDtoValidator : AbstractValidator<SliderUpdateDTO>
{
    public SliderUpdateDtoValidator()
    {
        RuleFor(x => x.Name).MaximumLength(40);
    }
}
