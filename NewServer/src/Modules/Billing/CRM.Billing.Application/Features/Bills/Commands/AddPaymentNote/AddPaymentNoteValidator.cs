// Ignore Spelling: Validator

using FluentValidation;

namespace CRM.Billing.Application.Features.Bills.Commands.AddPaymentNote;

public sealed class AddPaymentNoteValidator
    : AbstractValidator<AddPaymentNoteCommand>
{
    public AddPaymentNoteValidator()
    {
        RuleFor(x => x.BillId)
            .NotEmpty();

        RuleFor(x => x.PaymentId)
            .NotEmpty();

        RuleFor(x => x.PaymentAmount)
            .GreaterThan(0);

        RuleFor(x => x.MethodId)
            .IsInEnum();

        RuleFor(x => x.PaymentDate)
            .NotEmpty();
    }
}
