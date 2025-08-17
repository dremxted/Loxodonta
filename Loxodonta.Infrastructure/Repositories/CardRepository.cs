using Loxodonta.Domain.Cards;
using Loxodonta.Domain.Contracts;
using Loxodonta.Infrastructure.Persistence;

namespace Loxodonta.Infrastructure.Repositories
{
    public class CardRepository(ApplicationDbContext dbContext) : ICardRepository
    {
        public async Task<Card> CreateAsync(Card card)
        {
            await dbContext.AddAsync(card);
            await dbContext.SaveChangesAsync();

            return card;
        }
    }
}