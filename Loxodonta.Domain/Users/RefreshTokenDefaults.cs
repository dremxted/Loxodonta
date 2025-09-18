namespace Loxodonta.Domain.Users;

internal static class RefreshTokenDefaults
{
    public static DateTime ExpirationDefault => DateTime.UtcNow.AddDays(7);
}
