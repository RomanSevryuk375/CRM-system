using CRM.Shared.Abstractions.CQRS;

namespace CRM.Billing.Application.Features.Bills.Commands.AddPaymentNote;

public sealed record AddPaymentNoteCommand(
        Guid BillId,
        Guid PaymentId,
        decimal PaymentAmount,
        int MethodId,
        DateTimeOffset PaymentDate) : ICommand;
