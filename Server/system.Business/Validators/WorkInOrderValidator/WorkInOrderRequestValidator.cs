using FluentValidation;
using Shared.Contracts.WorkInOrder;

namespace CRMSystem.Business.Validators.WorkInOrderValidator;

public class WorkInOrderRequestValidator : AbstractValidator<WorkInOrderRequest>
{
    public WorkInOrderRequestValidator()
    {
        RuleFor(x => x.OrderId)
            .GreaterThan(0)
                .WithMessage("OrderId should be positive");

        RuleFor(x => x.JobId)
            .GreaterThan(0)
                .WithMessage("JobId should be positive");

        RuleFor(x => x.WorkerId)
            .GreaterThan(0)
                .WithMessage("WorkerId should be positive");

        RuleFor(x => x.StatusId)
            .IsInEnum()
                .WithMessage("StatusId should be a valid enum value");

        RuleFor(x => x.TimeSpent)
            .GreaterThan(0) 
                .WithMessage("Time Spent should be positive or zero");
    }
}