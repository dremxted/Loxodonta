using Loxodonta.Domain.Contracts;
using Loxodonta.Infrastructure.Persistence;
using Loxodonta.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Loxodonta.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddApplicationDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });
    }

    public static void AddInfrastructureRepositories(this IServiceCollection services)
    {
        services.AddScoped<ICardRepository, CardRepository>();
    }
}