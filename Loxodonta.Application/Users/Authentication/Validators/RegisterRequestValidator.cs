using FluentValidation;
using Loxodonta.Application.Extensions;
using Loxodonta.Application.Users.Authentication.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Loxodonta.Application.Users.Authentication.Validators;

public class RegisterRequestValidator : AbstractValidator<RegisterRequestDto>
{
    public RegisterRequestValidator(IOptions<PasswordOptions> passwordOptions)
    {
        RuleFor(r => r.Email)
            .EmailAddress();

        RuleFor(r => r.Password)
            .MinimumLength(passwordOptions.Value.RequiredLength)
            .WithMessage("Password should be at least " + 
            $"{passwordOptions.Value.RequiredLength} characters long.");

        RuleFor(r => r.Password)
            .RequiredUniqueChars(passwordOptions.Value.RequiredUniqueChars)
            .WithMessage($"Password must contain at least " +
            $"{passwordOptions.Value.RequiredUniqueChars} unique characters.");

        if(passwordOptions.Value.RequireNonAlphanumeric)
        {
            RuleFor(r => r.Password)
                .RequireNonAlphanumeric()
                .WithMessage("Password must contain at least one non-alphanumeric character.");
        }

        if(passwordOptions.Value.RequireLowercase)
        {
            RuleFor(r => r.Password)
                .RequireLowercase()
                .WithMessage("Password must contain at least one lower case letter.");
        }

        if (passwordOptions.Value.RequireUppercase)
        {
            RuleFor(r => r.Password)
                .RequireUppercase()
                .WithMessage("Password must contain at least one capital letter.");
        }

        if(passwordOptions.Value.RequireDigit)
        {
            RuleFor(r => r.Password)
                .RequireDigit()
                .WithMessage("Password must contain at least one digit.");
        }

    }
}
