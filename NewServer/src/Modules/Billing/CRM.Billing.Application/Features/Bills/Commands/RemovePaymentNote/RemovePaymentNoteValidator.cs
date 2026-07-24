// Ignore Spelling: Validator

using FluentValidation;

namespace CRM.Billing.Application.Features.Bills.Commands.RemovePaymentNote;

public sealed class RemovePaymentNoteValidator
    : AbstractValidator<RemovePaymentNoteCommand>
{
    public RemovePaymentNoteValidator()
    {
        RuleFor(x => x.BillId)
            .NotEmpty();

        RuleFor(x => x.PaymentNoteId)
            .NotEmpty();
    }
}
