using FluentValidation;
using Shared.Contracts.PartSet;

namespace CRMSystem.Business.Validators.PartSetValidator;

public class PartSetRequestValidator : AbstractValidator<PartSetRequest>
{
    public PartSetRequestValidator()
    {
        RuleFor(x => x.OrderId)
            .GreaterThan(0)
            .When(x => x.OrderId.HasValue)
                .WithMessage("OrderId should be positive");

        RuleFor(x => x.PositionId)
            .NotEmpty().NotNull()
                .WithMessage("PositionId should not be empty")
            .GreaterThan(0)
                .WithMessage("PositionId should be positive");

        RuleFor(x => x.ProposalId)
            .GreaterThan(0)
            .When(x => x.ProposalId.HasValue)
                .WithMessage("ProposalId should be positive");

        RuleFor(x => x.Quantity)
            .GreaterThan(0)
                .WithMessage("Quantity should be positive");

        RuleFor(x => x.SoldPrice)
            .GreaterThan(0)
                .WithMessage("Sold Price should be positive");
        
        RuleFor(x => x)
            .Must(x => 
                x.OrderId.HasValue
                || x.ProposalId.HasValue)
            .WithMessage("PartSet should belong to either an Order or a Proposal");
    }
}