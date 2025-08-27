using Loxodonta.Application.Cards;
using Loxodonta.Common;

namespace Loxodonta.Application.Contracts;

public interface ICardService
{
    Task<Result<CardDto>> CreateAsync(CreateCardDto createCardDto);
    Task<Result<CardDto>> FindAsync(Guid id);
    Task<Result<IEnumerable<CardDto>>> GetAllAsync();
    Task<Result<CardDto>> UpdateAsync(Guid id, UpdateCardDto updateCardDto);
    Task<Result> DeleteAsync(Guid id);
}
