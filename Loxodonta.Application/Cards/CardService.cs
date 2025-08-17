using Loxodonta.Application.Contracts;
using Loxodonta.Application.Extensions.Entities;
using Loxodonta.Common;
using Loxodonta.Domain.Contracts;

namespace Loxodonta.Application.Cards;

public class CardService(ICardRepository cardRepository) : ICardService
{
    public async Task<Result<CardDto>> CreateAsync(CreateCardDto createCardDto)
    {
        // Override unexpected order values to avoid gaps between them.
        createCardDto.Features = createCardDto.Features
            .OrderBy(f => f.Order)
            .Select((feature, index) =>
            {
                feature.Order = index;
                return feature;
            })
            .ToList();

        var card = await cardRepository.CreateAsync(createCardDto.ToEntity());
        return Result.Success(card.ToDto());
    }
}
