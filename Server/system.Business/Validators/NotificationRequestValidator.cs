using CRMSystem.Core.Validation;
using FluentValidation;
using Shared.Contracts.Notification;

namespace CRMSystem.Business.Validators;

public class NotificationRequestValidator : AbstractValidator<NotificationRequest>
{
    public NotificationRequestValidator()
    {
        RuleFor(x => x.ClientId)
            .GreaterThan(0)
                .WithMessage("ClientId should be positive");

        RuleFor(x => x.CarId)
            .GreaterThan(0)
                .WithMessage("CarId should be positive");

        RuleFor(x => x.TypeId)
            .IsInEnum()
                .WithMessage("TypeId should be a valid enum value");

        RuleFor(x => x.StatusId)
            .IsInEnum()
                .WithMessage("StatusId should be a valid enum value");

        RuleFor(x => x.Message)
            .NotEmpty()
                .WithMessage("Message should not be empty")
            .MaximumLength(ValidationConstants.MAX_DESCRIPTION_LENGTH)
                .WithMessage($"Message should be shorter than {ValidationConstants.MAX_DESCRIPTION_LENGTH}");

        RuleFor(x => x.SendAt)
            .NotEmpty()
                .WithMessage("SendAt should not be empty");
    }
}