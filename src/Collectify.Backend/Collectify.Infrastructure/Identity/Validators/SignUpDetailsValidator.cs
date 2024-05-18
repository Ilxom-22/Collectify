using Collectify.Application.Identity.Models.Dtos;
using FluentValidation;

namespace Collectify.Infrastructure.Identity.Validators;

public class SignUpDetailsValidator : AbstractValidator<SignUpDetails>
{
    public SignUpDetailsValidator()
    {
        RuleFor(details => details.EmailAddress)
            .NotEmpty()
            .EmailAddress();

        RuleFor(details => details.UserName)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(32);

        RuleFor(details => details.Password)
            .NotEmpty()
            .MinimumLength(8)
            .MaximumLength(16);
    }
}