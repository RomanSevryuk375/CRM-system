using CRMSystem.Core.Validation;
using FluentValidation;
using Shared.Contracts.User;
using Shared.Enums;

namespace CRMSystem.Business.Validators;

public class UserRequestValidator : AbstractValidator<UserRequest>
{
    public UserRequestValidator()
    {
        RuleFor(x => x.RoleId)
            .GreaterThan(0)
                .WithMessage("RoleId should be positive")
            .Must(x => Enum.IsDefined(typeof(RoleEnum), x))
                .WithMessage("RoleId should be a valid role");

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