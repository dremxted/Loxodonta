using Loxodonta.Application.Features;

namespace Loxodonta.Application.Cards;

public class CreateCardDto
{
    public List<CreateCardFeatureDto> Features { get; set; } = new();
}
