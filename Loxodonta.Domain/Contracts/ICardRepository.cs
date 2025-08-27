using Loxodonta.Domain.Cards;

namespace Loxodonta.Domain.Contracts;

public interface ICardRepository
{
    Task<Card> CreateAsync(Card card);
    Task<Card?> FindAsync(Guid id);
    Task<IEnumerable<Card>> GetAllAsync();
    void Delete(Card card);
    Task SaveChangesAsync();
}