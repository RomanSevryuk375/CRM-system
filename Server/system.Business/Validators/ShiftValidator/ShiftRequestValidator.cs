using CRMSystem.Core.Validation;
using FluentValidation;
using Shared.Contracts.Shift;

namespace CRMSystem.Business.Validators.ShiftValidator;

public class ShiftRequestValidator : AbstractValidator<ShiftRequest>
{
    public ShiftRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
                .WithMessage("Name should not be empty")
            .MaximumLength(ValidationConstants.MAX_TYPE_NAME)
                .WithMessage($"Name should be shorter than {ValidationConstants.MAX_TYPE_NAME}");

        RuleFor(x => x.StartAt)
            .NotEmpty()
                .WithMessage("Start Time should not be empty");

        RuleFor(x => x.EndAt)
            .NotEmpty()
                .WithMessage("End Time should not be empty")
            .GreaterThan(x => x.StartAt)
                .WithMessage("End Time should be greater than Start Time");
    }
}