using CRMSystem.Core.Validation;
using FluentValidation;
using Shared.Contracts.Client;

namespace CRMSystem.Business.Validators.ClientValidator;

public class ClientRequestValidator : AbstractValidator<ClientRequest>
{
    public ClientRequestValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().NotNull()
                .WithMessage("UserId should not be empty")
            .GreaterThan(0)
                .WithMessage("UserId should be positive");

        RuleFor(x => x.Name)
            .NotEmpty()
                .WithMessage("Name should not be empty")
            .MaximumLength(ValidationConstants.MAX_NAME_LENGTH)
                .WithMessage($"Name should be shorter than {ValidationConstants.MAX_NAME_LENGTH}");

        RuleFor(x => x.Surname)
            .NotEmpty()
                .WithMessage("Surname should not be empty")
            .MaximumLength(ValidationConstants.MAX_SURNAME_LENGTH)
                .WithMessage($"Surname should be shorter than {ValidationConstants.MAX_SURNAME_LENGTH}");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
                .WithMessage("Phone number should not be empty")
            .Matches(@"^(\+375|80)(29|44|33|25)\d{7}$")
                .WithMessage("Phone number should be in format +375XXXXXXXXX or 80XXXXXXXXX");

        RuleFor(x => x.Email)
            .EmailAddress()
            .When(x => !string.IsNullOrEmpty(x.Email))
                .WithMessage("Email should be a valid email address")
            .MaximumLength(ValidationConstants.MAX_EMAIL_LENGTH)
                .WithMessage($"Email should be shorter than {ValidationConstants.MAX_EMAIL_LENGTH}");
    }
}