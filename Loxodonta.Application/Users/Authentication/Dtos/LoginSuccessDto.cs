namespace Loxodonta.Application.Users.Authentication.Dtos;

public record LoginSuccessDto
{
    public required Guid UserId { get; init; }
    public required string AccessToken { get; init; }
    public required int ExpiresIn { get; init; }
    public string Username { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string? RefreshToken { get; init; }
}