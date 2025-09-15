using Loxodonta.Domain.Users;
using Microsoft.IdentityModel.Tokens;

namespace Loxodonta.Application.Contracts.Users.Jwt;

public interface IJwtProvider
{
    SecurityToken CreateToken(User user);
    string CreateRefreshToken();
}