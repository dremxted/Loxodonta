using Loxodonta.Application.Common;
using Loxodonta.Application.Contracts.Users.Jwt;
using Loxodonta.Domain.Users;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Loxodonta.Infrastructure.Repositories.Users.Authentication;

public class JwtProvider(IOptions<JwtConfiguration> jwtOptions) : IJwtProvider
{
    public SecurityToken CreateToken(User user)
    {
        SymmetricSecurityKey securityKey = new(
            Encoding.UTF8.GetBytes(jwtOptions.Value.Key));

        SigningCredentials credentials = new(
            securityKey,
            SecurityAlgorithms.HmacSha256);

        List<Claim> claims =
        [
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email!),

            .. user.UserRoles
                .Select(ur => new Claim(ClaimTypes.Role,ur.Role!.Name!)),
        ];

        var descriptor = new SecurityTokenDescriptor()
        {
            Issuer = jwtOptions.Value.Issuer,
            Audience = jwtOptions.Value.Audience,
            IssuedAt = DateTime.UtcNow,
            Expires = DateTime.UtcNow.AddDays(1),
            Subject = new ClaimsIdentity(claims),
            SigningCredentials = credentials
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.CreateToken(descriptor);
    }
    public string CreateRefreshToken()
    {
        return Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
    }
}