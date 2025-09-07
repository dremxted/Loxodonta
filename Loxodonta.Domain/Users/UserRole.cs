using Microsoft.AspNetCore.Identity;

namespace Loxodonta.Domain.Users;

public class UserRole : IdentityUserRole<Guid>
{
    public User? User { get; private set; }
    public Role? Role { get; private set; }
}
