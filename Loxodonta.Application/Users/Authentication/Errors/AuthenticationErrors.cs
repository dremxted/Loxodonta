using Loxodonta.Common;

namespace Loxodonta.Application.Users.Authentication.Errors;

public static class AuthenticationErrors
{
    public static Error InvalidCredentials() =>
        Error.Failure("Users.User", "Invalid credentials provided.");
    public static Error AccountLockedOut() =>
        Error.Failure("Users.User", "Account is locked out.");
    public static Error EmailNotConfirmed() =>
        Error.Failure("Users.User", "Email is not confirmed.");
    public static Error RequiresTwoFactor() =>
        Error.Failure("Users.User", "Two-factor authentication is required.");
    public static Error Failure(params object[] args) =>
        Error.Failure("Users.User", "Sign-in attempt failed.", args);

}
