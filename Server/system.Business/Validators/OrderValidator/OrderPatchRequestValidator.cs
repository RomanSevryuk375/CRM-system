using FluentValidation;
using Shared.Contracts.Order;

namespace CRMSystem.Business.Validators.OrderValidator;

public class OrderPatchRequestValidator : AbstractValidator<OrderPatchRequest>
{
    public OrderPatchRequestValidator()
    {
        RuleFor(x => x.OrderStatus)
            .IsInEnum()
                .WithMessage("OrderStatus should be a valid enum value");
    }
}