using FluentValidation;
using Shared.Contracts.Bill;
using Shared.Enums;

namespace CRMSystem.Business.Validators.BillValidator;

public class BillRequestValidator : AbstractValidator<BillRequest>
{
    public BillRequestValidator()
    {
        RuleFor(x => x.OrderId)
            .NotEmpty()
                .WithMessage("OrderId should not be empty")
            .GreaterThan(0)
                .WithMessage("OrderId should be positive");

        RuleFor(x => x.StatusId)
            .IsInEnum()
                .WithMessage("StatusId should be a valid enum value");

        RuleFor(x => x.CreatedAt)
            .NotEmpty()
                .WithMessage("CreatedAt should not be empty")
            .LessThanOrEqualTo(DateTime.Now)
                .WithMessage($"CreatedAt should be less than or equal to {DateTime.Now}");

        RuleFor(x => x.Amount)
            .NotNull()
                .WithMessage("Amount should not be null")
            .GreaterThanOrEqualTo(0)
                .WithMessage("Amount should be positive or zero");

        RuleFor(x => x.ActualBillDate)
            .Must((bill, actualDate) => 
                !actualDate.HasValue || actualDate.Value >= DateOnly.FromDateTime(bill.CreatedAt))
                .WithMessage("Actual Bill Date should not be earlier than CreatedAt date")
            .NotEmpty()
            .When(x => x.StatusId == BillStatusEnum.Paid)
                    .WithMessage("Actual Bill Date should not be empty when status is Paid");
    }
}