namespace Loxodonta.Application.Users.Authentication.Dtos;

public record RefreshTokenDto
{
    public string AccessToken = null!;
    public string? RefreshToken;
}
