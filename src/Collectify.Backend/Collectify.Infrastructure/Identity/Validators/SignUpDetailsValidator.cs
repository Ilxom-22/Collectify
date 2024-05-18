using Collectify.Application.Identity.Models.Dtos;
using FluentValidation;

namespace Collectify.Infrastructure.Identity.Validators;

public class SignUpDetailsValidator : AbstractValidator<SignUpDetails>
{
    public SignUpDetailsValidator()
    {
        RuleFor(details => details.UserName)
            .NotEmpty()
            .WithMessage("Username field can't be empty!")
            .MinimumLength(4)
            .WithMessage("Username should be at least 4 characters length!")
            .MaximumLength(32)
            .WithMessage("Username length should not exceed 32 characters!")
            .Matches("^[a-zA-Z][a-zA-Z0-9_]{2,30}[a-zA-Z0-9]$")
            .WithMessage("Username can only contain letters, numbers and underscores. Username can't start with an underscore or number and can't end with an underscore");
        
        RuleFor(details => details.EmailAddress)
            .NotEmpty()
            .WithMessage("Email Address field can't be empty!")
            .EmailAddress()
            .WithMessage("Invalid Email Address! Please try again!");

        RuleFor(details => details.Password)
            .NotEmpty()
            .WithMessage("Password field can't be empty!")
            .MinimumLength(8)
            .WithMessage("Password should be at least 8 characters length!")
            .MaximumLength(16)
            .WithMessage("Password length should not exceed 16 characters!");
    }
}