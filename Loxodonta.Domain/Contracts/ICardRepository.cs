using Loxodonta.Domain.Cards;

namespace Loxodonta.Domain.Contracts;

public interface ICardRepository
{
    Task<Card> CreateAsync(Card card);
}
