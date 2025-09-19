using FluentValidation;
using Loxodonta.Application.Users.Authentication.Dtos;

namespace Loxodonta.Application.Users.Authentication.Validators;

public class RegisterRequestValidator : AbstractValidator<RegisterRequestDto>
{
    public RegisterRequestValidator()
    {
        RuleFor(r => r.Email)
            .EmailAddress();
    }
}
