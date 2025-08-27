using Loxodonta.Application.Features;
using System.ComponentModel.DataAnnotations;

namespace Loxodonta.Application.Cards;

public class UpdateCardDto : IValidatableObject
{
    public List<UpdateCardFeatureDto> Features { get; set; } = new();

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (Features == null)
        {
            yield return new ValidationResult("null", [$"{nameof(Features)}"]);
            yield break;
        }
    }
}