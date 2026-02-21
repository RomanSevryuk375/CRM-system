using CRMSystem.Core.Validation;
using FluentValidation;
using Shared.Contracts.Worker;

namespace CRMSystem.Business.Validators.WorkerValidator;

public class WorkerRequestValidator : AbstractValidator<WorkerRequest>
{
    public WorkerRequestValidator()
    {
        RuleFor(x => x.UserId)
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
            .MaximumLength(ValidationConstants.MAX_NAME_LENGTH)
                .WithMessage($"Surname should be shorter than {ValidationConstants.MAX_NAME_LENGTH}");

        RuleFor(x => x.HourlyRate)
            .GreaterThan(0)
                .WithMessage("Hourly Rate should be positive");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
                .WithMessage("Phone number should not be empty")
            .Matches(@"^(\+375|80)(29|44|33|25)\d{7}$")
                .WithMessage("Phone number should be in format +375XXXXXXXXX or 80XXXXXXXXX");

        RuleFor(x => x.Email)
            .NotEmpty()
                .WithMessage("Email should not be empty")
            .EmailAddress()
                .WithMessage("Email should be a valid email address")
            .MaximumLength(ValidationConstants.MAX_EMAIL_LENGTH)
                .WithMessage($"Email should be shorter than {ValidationConstants.MAX_EMAIL_LENGTH}");
    }
}