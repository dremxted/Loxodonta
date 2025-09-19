using FluentValidation;
using Loxodonta.Application.Users.Authentication.Dtos;

namespace Loxodonta.Application.Users.Authentication.Validators;

internal class RegisterRequestValidator : AbstractValidator<RegisterRequestDto>
{
    internal RegisterRequestValidator()
    {
        RuleFor(r => r.Email)
            .EmailAddress();
    }
}
