using Loxodonta.Domain.Cards;
using Loxodonta.Domain.Contracts;
using Loxodonta.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

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

        public async Task<Card?> FindAsync(Guid id)
        {
            var card = await dbContext.Cards
                .Include(c => c.Features)
                .FirstOrDefaultAsync(card => card.Id == id);
            return card;
        }
        public async Task<IEnumerable<Card>> GetAllAsync()
        {
            return await dbContext.Cards
                .Include(card => card.Features)
                .ToListAsync();
        }

        public void Delete(Card card)
        {
            dbContext.Cards.Remove(card);
        }

        public async Task SaveChangesAsync()
        {
            await dbContext.SaveChangesAsync();
        }

    }
}