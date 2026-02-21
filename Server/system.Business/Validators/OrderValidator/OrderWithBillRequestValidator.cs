using FluentValidation;
using Shared.Contracts.Order;
using Shared.Enums;

namespace CRMSystem.Business.Validators.OrderValidator;

public class OrderWithBillRequestValidator : AbstractValidator<OrderWithBillRequest>
{
    public OrderWithBillRequestValidator()
    {
        RuleFor(x => x.OrderStatusId)
            .IsInEnum()
                .WithMessage("OrderStatusId should be a valid enum value");

        RuleFor(x => x.CarId)
            .GreaterThan(0)
                .WithMessage("CarId should be positive");

        RuleFor(x => x.Date)
            .NotEmpty()
                .WithMessage("Date should not be empty");

        RuleFor(x => x.PriorityId)
            .IsInEnum()
                .WithMessage("PriorityId should be a valid enum value");
        
        RuleFor(x => x.OrderId)
            .GreaterThan(0)
                .WithMessage("OrderId should be positive");

        RuleFor(x => x.BillStatusId)
            .IsInEnum()
                .WithMessage("BillStatusId should be a valid enum value");

        RuleFor(x => x.CreatedAt)
            .NotEmpty()
                .WithMessage("CreatedAt should not be empty");

        RuleFor(x => x.Amount)
            .Must(x => x >= 0)
                .WithMessage("Amount should be positive or zero");

        RuleFor(x => x.ActualBillDate)
            .Must((request, actualDate) => 
                !actualDate.HasValue || actualDate.Value >= request.Date)
                .WithMessage("Actual Bill Date should not be earlier than Order Date")
            .NotEmpty()
            .When(x => x.BillStatusId == BillStatusEnum.Paid)
                .WithMessage("Actual Bill Date should not be empty when status is Paid");
    }
}