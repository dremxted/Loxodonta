using Loxodonta.Domain.Contracts;
using Loxodonta.Domain.Users;
using Loxodonta.Infrastructure.Persistence;

namespace Loxodonta.Infrastructure.Repositories.Users.Authentication;

public class RefreshTokenRepository(ApplicationDbContext dbContext) : IRefreshTokenRepository
{
    public async Task<RefreshToken> CreateAsync(RefreshToken refreshToken)
    {
        await dbContext.RefreshTokens.AddAsync(refreshToken);
        return refreshToken;
    }

    public async Task SaveChangesAsync()
    {
        await dbContext.SaveChangesAsync();
    }
}