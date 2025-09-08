namespace Loxodonta.Application.Common;

/// <summary>
/// JwtConfiguration stores data of the Configuration's JWT related section.
/// </summary>
public record JwtConfiguration
{
    public required string Key { get; init; }
    public required string Issuer { get; init; }
    public required string Audience { get; init; }
}