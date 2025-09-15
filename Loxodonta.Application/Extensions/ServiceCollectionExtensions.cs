using FluentValidation;
using Loxodonta.Application.Cards;
using Loxodonta.Application.Contracts;
using Loxodonta.Application.Contracts.Users.Authentication;
using Loxodonta.Application.Users.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace Loxodonta.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        //FluentValidation
        var assembly = typeof(ServiceCollectionExtensions).Assembly;
        services.AddValidatorsFromAssembly(assembly);

        //Cards
        services.AddScoped<ICardService, CardService>();

        //Users
        services.AddScoped<IUserAuthenticationService, UserAuthenticationService>();
    }
}
