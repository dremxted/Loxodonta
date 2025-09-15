namespace Loxodonta.Application.Users.Authentication.Dtos;

public record LoginRequestDto
{
    public string Email { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
}