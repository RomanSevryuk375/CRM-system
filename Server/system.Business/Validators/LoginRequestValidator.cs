using CRMSystem.Core.Validation;
using FluentValidation;
using Shared.Contracts.Login;

namespace CRMSystem.Business.Validators;

public class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(x => x.Login)
            .NotEmpty()
                .WithMessage("Login should not be empty")
            .MaximumLength(ValidationConstants.MAX_NAME_LENGTH)
                .WithMessage($"Login should be shorter than {ValidationConstants.MAX_NAME_LENGTH}");

        RuleFor(x => x.Password)
            .NotEmpty()
                .WithMessage("Password should not be empty")
            .MinimumLength(6)
                .WithMessage("Password should be at least 6 characters long");
    }
}