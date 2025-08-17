using Loxodonta.Application.Cards;
using Loxodonta.Domain.Cards;

namespace Loxodonta.Application.Extensions.Entities;

public static class CreateDtoToEntityExtensions
{
    public static Card ToEntity(this CreateCardDto createDto)
    {
        var card = new Card();
        foreach (var feature in createDto.Features)
        {
            card.AddFeature(feature.Name,feature.Value,feature.Order);
        }

        return card;
    }
}
