using FluentValidation;
using Shared.Contracts.Bill;
using Shared.Enums;

namespace CRMSystem.Business.Validators.BillValidator;

public class BillUpdateRequestValidator : AbstractValidator<BillUpdateRequest>
{
    public BillUpdateRequestValidator()
    {
        RuleFor(x => x.StatusId)
            .IsInEnum()
            .When(x => x.StatusId.HasValue)
                .WithMessage("StatusId should be a valid enum value");

        RuleFor(x => x.Amount)
            .GreaterThanOrEqualTo(0)
            .When(x => x.Amount.HasValue)
                .WithMessage("Amount should be positive or zero");

        RuleFor(x => x.ActualBillDate)
            .NotEmpty()
            .When(x => x.StatusId == BillStatusEnum.Paid)
                .WithMessage("Actual Bill Date should not be empty when status is Paid");
        
        RuleFor(x => x)
            .Must(x => 
                x.StatusId.HasValue
                || x.Amount.HasValue 
                || x.ActualBillDate.HasValue)
            .WithMessage("At least one field should be provided for update");
    }
}