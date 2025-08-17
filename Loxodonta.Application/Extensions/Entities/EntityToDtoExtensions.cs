using Loxodonta.Application.Cards;
using Loxodonta.Application.Features;
using Loxodonta.Domain.Cards;

namespace Loxodonta.Application.Extensions.Entities;

public static class EntityToDtoExtensions
{
    public static CardDto ToDto(this Card card)
    {
        return new CardDto()
        {
            Id = card.Id,
            Features = card.Features
                .Select(f => f.ToDto())
                .ToList()
        };
    }

    public static FeatureDto ToDto(this Feature feature)
    {
        return new FeatureDto()
        {
            Id = feature.Id,
            Name = feature.Name,
            Value = feature.Value,
            Order = feature.Order
        };
    }
}