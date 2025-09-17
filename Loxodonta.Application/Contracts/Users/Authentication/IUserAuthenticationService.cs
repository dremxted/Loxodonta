using Loxodonta.Application.Users.Authentication.Dtos;
using Loxodonta.Common;

namespace Loxodonta.Application.Contracts.Users.Authentication;

public interface IUserAuthenticationService
{
    Task<Result<LoginSuccessDto>> LoginUser(LoginRequestDto signInUserDto);
    Task<Result<RegisterSuccessDto>> RegisterUser(RegisterRequestDto registerRequestDto);
    Task<Result<RefreshTokenDto>> RefreshTokenAsync(string refreshToken);
}