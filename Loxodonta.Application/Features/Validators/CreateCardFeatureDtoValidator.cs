using FluentValidation;

namespace Loxodonta.Application.Features.Validators;

public class CreateCardFeatureDtoValidator : AbstractValidator<CreateCardFeatureDto>
{
    public CreateCardFeatureDtoValidator()
    {
        RuleFor(feature => feature.Name)
            .MaximumLength(255)
            .WithMessage("Length of the value is greater than 255.");

        RuleFor(feature => feature.Value)
            .MaximumLength(255)
            .WithMessage("Length of the value is greater than 255.");
    }
}
