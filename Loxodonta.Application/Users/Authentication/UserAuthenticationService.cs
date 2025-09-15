using Loxodonta.Application.Contracts.Users.Authentication;
using Loxodonta.Application.Contracts.Users.Jwt;
using Loxodonta.Application.Extensions.Entities;
using Loxodonta.Application.Users.Authentication.Dtos;
using Loxodonta.Application.Users.Authentication.Errors;
using Loxodonta.Common;
using Loxodonta.Domain.Contracts;
using Loxodonta.Domain.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace Loxodonta.Application.Users.Authentication;
public class UserAuthenticationService(
    IJwtProvider tokenProvider,
    UserManager<User> userManager,
    SignInManager<User> signInManager,
    IRefreshTokenRepository refreshTokenRepository) 
    : IUserAuthenticationService
{
    public async Task<Result<LoginSuccessDto>> LoginUser(LoginRequestDto loginRequestDto)
    {
        User? user = await userManager.FindByEmailAsync(loginRequestDto.Email);

        if(user is null)
        {
            return Result.Failure<LoginSuccessDto>(AuthenticationErrors.InvalidCredentials());
        }

        SignInResult result = await signInManager
            .PasswordSignInAsync(user, loginRequestDto.Password, false, true);

        if (!result.Succeeded)
        {
            return Result.Failure<LoginSuccessDto>(AuthenticationErrors.Failure());
        }

        SecurityToken? token = tokenProvider.CreateToken(user);
        JwtSecurityTokenHandler tokenHandler = new();
        string tokenString = tokenHandler.WriteToken(token);
        int expiresIn = (int)(token.ValidTo - DateTime.UtcNow).TotalSeconds;

        RefreshToken refreshToken = new(user, tokenProvider.CreateRefreshToken());

        await refreshTokenRepository.CreateAsync(refreshToken);
        await refreshTokenRepository.SaveChangesAsync();

        return Result.Success(new LoginSuccessDto()
        {
            UserId = user.Id,
            AccessToken = tokenString,
            ExpiresIn = expiresIn,
            Username = user.UserName!,
            Email = user.Email!,
            RefreshToken = refreshToken.Token
        });
    }

    public async Task<Result<RegisterSuccessDto>> RegisterUser(RegisterRequestDto registerRequestDto)
    {
        User user = new()
        {
            UserName = registerRequestDto.UserName,
            Email = registerRequestDto.Email,
        };

        IdentityResult result = await userManager
            .CreateAsync(user, registerRequestDto.Password);

        if (!result.Succeeded)
        {
            IEnumerable<IdentityError> errors = result.Errors;
            throw new NotImplementedException();
        }

        return Result.Success(new RegisterSuccessDto());
    }
}
