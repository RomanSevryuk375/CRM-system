// Ignore Spelling: Validator

using FluentValidation;

namespace CRM.Billing.Application.Features.Bills.Commands.CloseBillManually;

public sealed class CloseBillManuallyValidator
    : AbstractValidator<CloseBillManuallyCommand>
{
    public CloseBillManuallyValidator()
    {
        RuleFor(x => x.BillId)
            .NotEmpty();
    }
}
