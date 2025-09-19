using Loxodonta.API.Validation.Filters;
using Loxodonta.Application.Contracts.Users.Authentication;
using Loxodonta.Application.Users.Authentication.Dtos;
using Loxodonta.Common;
using Microsoft.AspNetCore.Mvc;

namespace Loxodonta.API.Controllers;

[ApiController]
public class AuthenticationController(IUserAuthenticationService authService) : ControllerBase
{
    [HttpPost("api/login")]
    public async Task<IActionResult> LoginUser(LoginRequestDto loginRequest)
    {
        Result<LoginSuccessDto> result = await authService.LoginUser(loginRequest);
        return result.Match<IActionResult>(Ok, BadRequest);
    }

    [HttpPost("api/register")]
    [ServiceFilter(typeof(ValidationFilter<RegisterRequestDto>))]
    public async Task<IActionResult> RegisterUser(RegisterRequestDto registerRequest)
    {
        Result<RegisterSuccessDto> result = await authService.RegisterUser(registerRequest);
        return result.Match<IActionResult>(Created, BadRequest);
    }

    [HttpPost("api/refresh-token")]
    public async Task<IActionResult> RefreshTokenAsync(string refreshToken)
    {
        Result<RefreshTokenDto> result = await authService.RefreshTokenAsync(refreshToken);
        return result.Match<IActionResult>(Ok,BadRequest);
    }
}