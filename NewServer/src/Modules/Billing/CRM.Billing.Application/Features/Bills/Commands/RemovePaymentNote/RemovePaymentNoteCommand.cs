using CRM.Shared.Abstractions.CQRS;

namespace CRM.Billing.Application.Features.Bills.Commands.RemovePaymentNote;

public sealed record RemovePaymentNoteCommand(
    Guid BillId,
    Guid PaymentNoteId) : ICommand;
