namespace Loxodonta.Domain.Users;

public class RefreshToken
{
    public string Token { get; private set; } = null!;
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public DateTime ExpiresOn { get; private set; }
    public User User { get; private set; } = null!;

    public bool HasExpired => DateTime.UtcNow >= ExpiresOn;

    private RefreshToken() { }

    public RefreshToken(User user, string token)
    {
        User = user;
        UserId = user.Id;
        Token = token;
        ExpiresOn = DateTime.UtcNow.AddDays(7);
    }
    public RefreshToken(User user, string token, DateTime expiresOn)
    {
        User = user;
        UserId = user.Id;
        Token = token;
        ExpiresOn = expiresOn;
    }
}