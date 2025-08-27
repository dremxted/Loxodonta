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

    public async Task<Result<CardDto>> FindAsync(Guid id)
    {
        var card = await cardRepository.FindAsync(id);
        if (card == null)
        {
            return Result.Failure<CardDto>(Error.NotFound("Cards","Not found."));
        }

        return Result.Success(card.ToDto());
    }

    public async Task<Result<IEnumerable<CardDto>>> GetAllAsync()
    {
        var allCards = await cardRepository.GetAllAsync();
        var cardsDto = allCards.Select(card => card.ToDto());
        return Result.Success(cardsDto);
    }

    public async Task<Result<CardDto>> UpdateAsync(Guid id, UpdateCardDto updateCardDto)
    {
        var card = await cardRepository.FindAsync(id);
        if (card == null)
        {
            return Result.Failure<CardDto>(Error.NotFound("Cards", "Not found."));
        }

        card.ClearFeatures();

        foreach (var (feature, order) in updateCardDto.Features
                .OrderBy(feature => feature.Order)
                .Select((feature, index) => (feature, index)))
        {
            card.AddFeature(feature.Name, feature.Value, order);
        }

        await cardRepository.SaveChangesAsync();
        return Result.Success(card.ToDto());
    }

    public async Task<Result> DeleteAsync(Guid id)
    {
        var deleteCard = await cardRepository.FindAsync(id);
        if(deleteCard == null)
        {
            return Result.Failure(Error.NotFound("Cards", "Not found."));
        }

        cardRepository.Delete(deleteCard);
        await cardRepository.SaveChangesAsync();
    
        return Result.Success();
    }
}
