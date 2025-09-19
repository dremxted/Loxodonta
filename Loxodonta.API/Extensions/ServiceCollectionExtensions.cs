using Loxodonta.API.Validation.Contracts;
using Loxodonta.API.Validation.Filters;
using Loxodonta.API.Validation.ModelStateResultFactories;
using Loxodonta.Application.Cards;
using Loxodonta.Application.Users.Authentication.Dtos;

namespace Loxodonta.API.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddValidationFilters(this IServiceCollection services)
    {
        services.AddScoped<IValidationObjectResultFactory, ValidationBadRequestFactory>();
        services.AddScoped<ValidationFilter<CreateCardDto>>();
        services.AddScoped<ValidationFilter<UpdateCardDto>>();
        services.AddScoped<ValidationFilter<RegisterRequestDto>>();
    }
}
