using CRMSystem.Core.Validation;
using FluentValidation;
using Shared.Contracts.Client;

namespace CRMSystem.Business.Validators.ClientValidator;

public class ClientUpdateRequestValidator : AbstractValidator<ClientUpdateRequest>
{
    public ClientUpdateRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .When(x => x.Name is not null)
                .WithMessage("Name should not be empty")
            .MaximumLength(ValidationConstants.MAX_NAME_LENGTH)
            .When(x => x.Name is not null)
                .WithMessage($"Name should be shorter than {ValidationConstants.MAX_NAME_LENGTH}");

        RuleFor(x => x.Surname)
            .NotEmpty()
            .When(x => x.Surname is not null)
                .WithMessage("Surname should not be empty")
            .MaximumLength(ValidationConstants.MAX_SURNAME_LENGTH)
            .When(x => x.Surname is not null)
                .WithMessage($"Surname should be shorter than {ValidationConstants.MAX_SURNAME_LENGTH}");

        RuleFor(x => x.PhoneNumber)
            .Matches(@"^(\+375|80)(29|44|33|25)\d{7}$")
            .When(x => !string.IsNullOrEmpty(x.PhoneNumber))
                .WithMessage("Phone number should be in format +375XXXXXXXXX or 80XXXXXXXXX");

        RuleFor(x => x.Email)
            .EmailAddress()
            .When(x => !string.IsNullOrEmpty(x.Email))
                .WithMessage("Email should be a valid email address");

        RuleFor(x => x)
            .Must(x => 
                x.Name is not null 
                || x.Surname is not null 
                || x.PhoneNumber is not null 
                || x.Email is not null)
            .WithMessage("At least one field should be provided for update");
    }
}