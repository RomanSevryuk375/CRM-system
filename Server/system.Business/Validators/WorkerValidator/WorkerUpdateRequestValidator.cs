using CRMSystem.Core.Validation;
using FluentValidation;
using Shared.Contracts.Worker;

namespace CRMSystem.Business.Validators.WorkerValidator;

public class WorkerUpdateRequestValidator : AbstractValidator<WorkerUpdateRequest>
{
    public WorkerUpdateRequestValidator()
    {
        RuleFor(x => x.UserId)
            .GreaterThan(0)
            .When(x => x.UserId.HasValue)
                .WithMessage("UserId should be positive");

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
            .MaximumLength(ValidationConstants.MAX_NAME_LENGTH)
            .When(x => x.Surname is not null)
                .WithMessage($"Surname should be shorter than {ValidationConstants.MAX_NAME_LENGTH}");

        RuleFor(x => x.HourlyRate)
            .GreaterThan(0)
            .When(x => x.HourlyRate.HasValue)
                .WithMessage("Hourly Rate should be positive");

        RuleFor(x => x.PhoneNumber)
            .Matches(@"^(\+375|80)(29|44|33|25)\d{7}$")
            .When(x => x.PhoneNumber is not null)
                .WithMessage("Phone number should be in format +375XXXXXXXXX or 80XXXXXXXXX");

        RuleFor(x => x.Email)
            .EmailAddress()
            .When(x => x.Email is not null)
                .WithMessage("Email should be a valid email address")
            .MaximumLength(ValidationConstants.MAX_EMAIL_LENGTH)
            .When(x => x.Email is not null)
                .WithMessage($"Email should be shorter than {ValidationConstants.MAX_EMAIL_LENGTH}");

        RuleFor(x => x)
            .Must(x => 
                x.UserId.HasValue 
                || x.Name is not null
                || x.Surname is not null 
                || x.HourlyRate.HasValue 
                || x.PhoneNumber is not null 
                || x.Email is not null)
            .WithMessage("At least one field should be provided for update");
    }
}