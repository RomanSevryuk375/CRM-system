using CRMSystem.Core.Validation;
using FluentValidation;
using Shared.Contracts.Shift;

namespace CRMSystem.Business.Validators.ShiftValidator;

public class ShiftUpdateRequestValidator : AbstractValidator<ShiftUpdateRequest>
{
    public ShiftUpdateRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .When(x => x.Name is not null)
                .WithMessage("Name should not be empty")
            .MaximumLength(ValidationConstants.MAX_TYPE_NAME)
            .When(x => x.Name is not null)
                .WithMessage($"Name should be shorter than {ValidationConstants.MAX_TYPE_NAME}");

        RuleFor(x => x.EndAt)
            .GreaterThan(x => x.StartAt!.Value)
            .When(x => x.StartAt.HasValue && x.EndAt.HasValue)
                .WithMessage("End Time should be greater than Start Time");

        RuleFor(x => x)
            .Must(x => 
                x.Name is not null 
                || x.StartAt.HasValue 
                || x.EndAt.HasValue)
            .WithMessage("At least one field should be provided for update");
    }
}