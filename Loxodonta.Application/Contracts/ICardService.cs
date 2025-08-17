using Loxodonta.Application.Cards;
using Loxodonta.Common;

namespace Loxodonta.Application.Contracts;

public interface ICardService
{
    Task<Result<CardDto>> CreateAsync(CreateCardDto createCardDto);
}
