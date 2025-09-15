using Loxodonta.Domain.Contracts;
using Loxodonta.Domain.Users;
using Loxodonta.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Loxodonta.Infrastructure.Repositories.Users.Authentication;

public class RefreshTokenRepository(ApplicationDbContext dbContext) : IRefreshTokenRepository
{
    public async Task<RefreshToken> CreateAsync(RefreshToken refreshToken)
    {
        await dbContext.RefreshTokens.AddAsync(refreshToken);
        return refreshToken;
    }

    public async Task<RefreshToken?> FindByValueAsync(string token)
    {
        var refreshToken = await dbContext.RefreshTokens
            .Include(t => t.User)
            .FirstOrDefaultAsync(t => t.Token == token);
        return refreshToken;
    }

    public async Task SaveChangesAsync()
    {
        await dbContext.SaveChangesAsync();
    }
}