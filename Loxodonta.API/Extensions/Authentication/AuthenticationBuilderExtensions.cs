using Loxodonta.Application.Common;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Loxodonta.API.Extensions.Authentication;

public static class AuthenticationBuilderExtensions
{
    public static AuthenticationBuilder AddJwtBearerConfigured(
        this AuthenticationBuilder authBuilder,
        IConfigurationSection configurationSection)
    {
        if (configurationSection is null || !configurationSection.Exists())
        {
            throw new ArgumentNullException(
                nameof(configurationSection),
                "JWT configuration section not found or is empty");
        }

        var jwtConfiguration = configurationSection
            .Get<JwtConfiguration>()!;

        return authBuilder.AddJwtBearer(jwtOptions =>
        {
            jwtOptions.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidIssuer = jwtConfiguration.Issuer,
                ValidAudience = jwtConfiguration.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(jwtConfiguration.Key!)),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true
            };
        });
    }
}
