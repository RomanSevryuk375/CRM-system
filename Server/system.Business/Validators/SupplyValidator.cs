using FluentValidation;
using Shared.Contracts.Supply;

namespace CRMSystem.Business.Validators;

public class SupplyRequestValidator : AbstractValidator<SupplyRequest>
{
    public SupplyRequestValidator()
    {
        RuleFor(x => x.SupplierId)
            .GreaterThan(0)
                .WithMessage("SupplierId should be positive");

        RuleFor(x => x.Date)
            .NotEmpty()
                .WithMessage("Date should not be empty");
    }
}