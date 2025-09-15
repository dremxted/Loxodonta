namespace Loxodonta.Domain.Users;

public class RefreshToken
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public string Token { get; private set; } = null!;
    public User User { get; private set; } = null!;

    private RefreshToken() { }
    public RefreshToken(User user, string token)
    {
        User = user;
        UserId = user.Id;
        Token = token;
    }
}