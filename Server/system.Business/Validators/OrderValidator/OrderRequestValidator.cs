using FluentValidation;
using Shared.Contracts.Order;

namespace CRMSystem.Business.Validators.OrderValidator;

public class OrderRequestValidator : AbstractValidator<OrderRequest>
{
    public OrderRequestValidator()
    {
        RuleFor(x => x.StatusId)
            .IsInEnum()
                .WithMessage("StatusId should be a valid enum value");

        RuleFor(x => x.CarId)
            .GreaterThan(0)
                .WithMessage("CarId should be positive");

        RuleFor(x => x.Date)
            .NotEmpty()
                .WithMessage("Date should not be empty");

        RuleFor(x => x.PriorityId)
            .IsInEnum()
                .WithMessage("PriorityId should be a valid enum value");
    }
}