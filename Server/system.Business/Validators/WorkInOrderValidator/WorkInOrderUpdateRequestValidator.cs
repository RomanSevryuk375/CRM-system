using FluentValidation;
using Shared.Contracts.WorkInOrder;

namespace CRMSystem.Business.Validators.WorkInOrderValidator;

public class WorkInOrderUpdateRequestValidator : AbstractValidator<WorkInOrderUpdateRequest>
{
    public WorkInOrderUpdateRequestValidator()
    {
        RuleFor(x => x.WorkerId)
            .GreaterThan(0)
            .When(x => x.WorkerId.HasValue)
                .WithMessage("WorkerId should be positive");

        RuleFor(x => x.StatusId)
            .IsInEnum()
            .When(x => x.StatusId.HasValue)
                .WithMessage("StatusId should be a valid enum value");

        RuleFor(x => x.TimeSpent)
            .GreaterThan(0)
            .When(x => x.TimeSpent.HasValue)
                .WithMessage("Time Spent should be positive or zero");

        RuleFor(x => x)
            .Must(x =>
                x.WorkerId.HasValue 
                || x.StatusId.HasValue 
                || x.TimeSpent.HasValue)
            .WithMessage("At least one field should be provided for update");
    }
}