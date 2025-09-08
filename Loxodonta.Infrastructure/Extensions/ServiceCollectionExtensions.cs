using Loxodonta.Domain.Contracts;
using Loxodonta.Domain.Users;
using Loxodonta.Infrastructure.Persistence;
using Loxodonta.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Loxodonta.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructureDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });

        services.AddIdentity<User, Role>()
            .AddEntityFrameworkStores<ApplicationDbContext>();
    }

    public static void AddInfrastructureRepositories(this IServiceCollection services)
    {
        services.AddScoped<ICardRepository, CardRepository>();
    }
}