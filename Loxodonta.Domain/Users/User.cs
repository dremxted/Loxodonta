using Microsoft.AspNetCore.Identity;

namespace Loxodonta.Domain.Users;

public class User : IdentityUser<Guid>
{
    public List<UserRole> UserRoles { get; private set; } = new();
}
