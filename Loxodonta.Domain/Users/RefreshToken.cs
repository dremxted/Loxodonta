namespace Loxodonta.Domain.Users;

public class RefreshToken
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Token { get; set; } = null!;
    public User User { get; set; } = null!;
}