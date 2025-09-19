using Loxodonta.Domain.Users;
using Loxodonta.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Loxodonta.Infrastructure.Extensions;

public static class IdentityExtensions
{
    public static void AddPasswordOptions(this IServiceCollection services, IConfiguration configuration)
    {
        var passwordOptionsSection = configuration.GetSection(nameof(PasswordOptions));
        services.Configure<PasswordOptions>(passwordOptionsSection);
    }

    public static void AddIdentityConfigured(this IServiceCollection services, IConfiguration configuration)
    {
        var passwordSection = configuration.GetSection(nameof(PasswordOptions));
        var passwordOptions = passwordSection.Get<PasswordOptions>();

        if(passwordOptions is null)
        {
            throw new NullReferenceException(nameof(passwordOptions));
        }

        services.AddIdentity<User,Role>(options =>
        {
            options.User.RequireUniqueEmail = true;
            options.Password = passwordOptions;
        }).AddEntityFrameworkStores<ApplicationDbContext>();
    }
}
