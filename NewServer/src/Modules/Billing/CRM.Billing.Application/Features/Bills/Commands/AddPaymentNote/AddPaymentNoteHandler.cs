using CRM.Billing.Domain.Entities;
using CRM.Billing.Domain.Enums;
using CRM.Billing.Domain.Interfaces;
using CRM.Shared.Abstractions.Abstractions;
using CRM.Shared.Abstractions.CQRS;
using CRM.Shared.Abstractions.Results;

namespace CRM.Billing.Application.Features.Bills.Commands.AddPaymentNote;

public sealed class AddPaymentNoteHandler(
    IBillRepository billRepository,
    TimeProvider timeProvider)
    : ICommandHandler<AddPaymentNoteCommand>
{
    public async Task<Result> Handle(AddPaymentNoteCommand request, CancellationToken cancellationToken)
    {
        BillId billId = new(request.BillId);
        PaymentNoteId paymentNoteId = new(request.PaymentId);
        PaymentMethod paymentMethod = (PaymentMethod)request.MethodId;
        DateTimeOffset today = timeProvider.GetUtcNow();

        Bill? bill = await billRepository.GetByIdAsync(billId, cancellationToken);
        if (bill is null)
        {
            return Result.Failure(Error.NotFound<Bill>(
                "The specified bill was not found."));
        }

        Result result = bill.AddPaymentNote(
            paymentNoteId,
            request.PaymentAmount,
            paymentMethod,
            request.PaymentDate,
            today);
        if (result.IsFailure)
        {
            return result;
        }

        return Result.Success();
    }
}
