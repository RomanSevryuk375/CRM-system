// Ignore Spelling: Validator

using FluentValidation;

namespace CRM.Billing.Application.Features.Bills.Commands.CreateBill;

public sealed class CreateBillValidator
    : AbstractValidator<CreateBillCommand>
{
    public CreateBillValidator()
    {
        RuleFor(x => x.BillId)
            .NotEmpty();

        RuleFor(x => x.OrderId)
            .NotEmpty();

        RuleFor(x => x.StatusId)
            .IsInEnum()
            .NotEmpty();

        RuleFor(x => x.Amount)
            .GreaterThan(0);
    }
}
