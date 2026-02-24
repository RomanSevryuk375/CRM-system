using FluentValidation;
using Shared.Contracts.PaymentNote;

namespace CRMSystem.Business.Validators;

public class PaymentNoteRequestValidator : AbstractValidator<PaymentNoteRequest>
{
    public PaymentNoteRequestValidator()
    {
        RuleFor(x => x.BillId)
            .NotEmpty().NotNull()
                .WithMessage("BillId should not be empty")
            .GreaterThan(0)
                .WithMessage("BillId should be positive");

        RuleFor(x => x.Date)
            .NotEmpty()
                .WithMessage("Date should not be empty")
            .LessThanOrEqualTo(DateTime.Now)
                .WithMessage($"Date should be less than or equal to {DateTime.Now}");

        RuleFor(x => x.Amount)
            .GreaterThan(0)
                .WithMessage("Amount should be positive");

        RuleFor(x => x.MethodId)
            .IsInEnum()
                .WithMessage("MethodId should be a valid enum value");
    }
}