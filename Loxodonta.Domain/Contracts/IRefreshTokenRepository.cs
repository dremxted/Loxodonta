using Loxodonta.Domain.Users;

namespace Loxodonta.Domain.Contracts;

public interface IRefreshTokenRepository
{
    Task<RefreshToken> CreateAsync(RefreshToken refreshToken);
    Task<RefreshToken?> FindByValueAsync(string token)
    Task SaveChangesAsync();
}