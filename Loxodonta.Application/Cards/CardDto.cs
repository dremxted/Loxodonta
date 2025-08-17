using Loxodonta.Application.Features;

namespace Loxodonta.Application.Cards;

public class CardDto
{
    public Guid Id { get; set; }
    public List<FeatureDto> Features { get; set; } = new();
}