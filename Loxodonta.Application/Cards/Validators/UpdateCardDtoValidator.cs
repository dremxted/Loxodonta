using FluentValidation;
using Loxodonta.Application.Extensions;

namespace Loxodonta.Application.Cards.Validators;

public class UpdateCardDtoValidator : AbstractValidator<UpdateCardDto>
{
    public UpdateCardDtoValidator()
    {
        RuleFor(card => card.Features)
            .MustContainUniqueValue(feature => feature.Order)
            .WithMessage("Order values are not unique.");
    }
}
