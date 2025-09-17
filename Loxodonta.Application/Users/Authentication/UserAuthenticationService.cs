using Loxodonta.Application.Contracts.Users.Authentication;
using Loxodonta.Application.Contracts.Users.Jwt;
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

        RefreshToken refreshToken = new(user, tokenProvider.CreateRefreshToken());
        await refreshTokenRepository.CreateAsync(refreshToken);
        await refreshTokenRepository.SaveChangesAsync();

        int expiresInSeconds = (int)(token.ValidTo - DateTime.UtcNow).TotalSeconds;
        return Result.Success(new LoginSuccessDto()
        {
            UserId = user.Id,
            AccessToken = tokenString,
            ExpiresIn = expiresInSeconds,
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
    
    public async Task<Result<RefreshTokenDto>> RefreshTokenAsync(string refreshToken)
    {
        RefreshToken? dbRefreshToken = await refreshTokenRepository.FindByValueAsync(refreshToken);

        if(dbRefreshToken is null || dbRefreshToken.HasExpired)
        {
            return Result.Failure<RefreshTokenDto>(
                Error.Failure("User.RefreshToken", "Refresh Token has expired."));
        }

        SecurityToken? accessToken = tokenProvider.CreateToken(dbRefreshToken.User);
        JwtSecurityTokenHandler tokenHandler = new();
        string tokenString = tokenHandler.WriteToken(accessToken);

        RefreshToken newRefreshToken = new(
            dbRefreshToken.User, 
            tokenProvider.CreateRefreshToken(),
            DateTime.UtcNow.AddDays(7));

        await refreshTokenRepository.CreateAsync(newRefreshToken);
        await refreshTokenRepository.SaveChangesAsync();

        RefreshTokenDto response = new()
        {

            AccessToken = tokenString,
            RefreshToken = newRefreshToken.Token
        };

        return Result.Success(response);
    }
}