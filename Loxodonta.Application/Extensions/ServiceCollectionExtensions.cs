using Loxodonta.Application.Cards;
using Loxodonta.Application.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace Loxodonta.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<ICardService, CardService>();
    }
}
