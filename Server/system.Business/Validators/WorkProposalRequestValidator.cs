using FluentValidation;
using Shared.Contracts.WorkProposal;

namespace CRMSystem.Business.Validators;

public class WorkProposalRequestValidator : AbstractValidator<WorkProposalRequest>
{
    public WorkProposalRequestValidator()
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

        RuleFor(x => x.Date)
            .NotEmpty()
                .WithMessage("Date should not be empty")
            .LessThanOrEqualTo(DateTime.Now)
                .WithMessage($"Date should be less than or equal to {DateTime.Now}");
    }
}