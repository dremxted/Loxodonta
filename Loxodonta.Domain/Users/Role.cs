using Microsoft.AspNetCore.Identity;

namespace Loxodonta.Domain.Users;

public class Role : IdentityRole<Guid>
{
    public List<UserRole> UserRoles { get; private set; } = new();
}
