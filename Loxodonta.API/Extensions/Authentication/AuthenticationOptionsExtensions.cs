using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Loxodonta.API.Extensions.Authentication;

public static class AuthenticationOptionsExtensions
{
    public static void UseDefaults(AuthenticationOptions authOptions)
    {
        authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        authOptions.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    }
}