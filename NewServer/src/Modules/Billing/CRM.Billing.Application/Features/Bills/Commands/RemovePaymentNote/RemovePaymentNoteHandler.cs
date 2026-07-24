using CRM.Billing.Domain.Entities;
using CRM.Billing.Domain.Interfaces;
using CRM.Shared.Abstractions.Abstractions;
using CRM.Shared.Abstractions.Results;
using MediatR;

namespace CRM.Billing.Application.Features.Bills.Commands.RemovePaymentNote;

public sealed class RemovePaymentNoteHandler(
    IBillRepository billRepository,
    TimeProvider timeProvider)
    : IRequestHandler<RemovePaymentNoteCommand, Result>
{
    public async Task<Result> Handle(RemovePaymentNoteCommand request, CancellationToken cancellationToken)
    {
        BillId billId = new(request.BillId);
        Bill? bill = await billRepository.GetByIdAsync(billId, cancellationToken);
        if (bill is null)
        {
            return Result.Success();
        }

        DateTimeOffset today = timeProvider.GetUtcNow();
        PaymentNoteId paymentNoteId = new(request.PaymentNoteId);
        Result result = bill.RemovePaymentNote(paymentNoteId, today);
        if (result.IsFailure)
        {
            return Result.Failure(result.Error);
        }

        return Result.Success();
    }
}
