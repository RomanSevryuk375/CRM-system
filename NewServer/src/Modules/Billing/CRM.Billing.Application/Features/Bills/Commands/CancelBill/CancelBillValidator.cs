// Ignore Spelling: Validator

using FluentValidation;

namespace CRM.Billing.Application.Features.Bills.Commands.CancelBill;

public sealed class CancelBillValidator
    : AbstractValidator<CancelBillCommand>
{
    public CancelBillValidator()
    {
        RuleFor(x => x.BillId)
            .NotEmpty();
    }
}
