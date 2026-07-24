using CRM.Billing.Domain.Entities;
using CRM.Billing.Domain.Interfaces;
using CRM.Shared.Abstractions.Abstractions;
using CRM.Shared.Abstractions.Results;
using MediatR;

namespace CRM.Billing.Application.Features.Bills.Commands.CloseBillManually;

public sealed class CloseBillManuallyHandler(
    IBillRepository billRepository,
    TimeProvider timeProvider)
    : IRequestHandler<CloseBillManuallyCommand, Result>
{
    public async Task<Result> Handle(CloseBillManuallyCommand request, CancellationToken cancellationToken)
    {
        BillId billId = new(request.BillId);
        DateTimeOffset today = timeProvider.GetUtcNow();
        Bill? bill = await billRepository.GetByIdAsync(billId, cancellationToken);
        if (bill is null)
        {
            return Result.Failure(Error.NotFound<Bill>(
                $"Bill {request.BillId} not found."));
        }

        Result result = bill.CloseByUser(DateOnly.FromDateTime(today.Date));
        if (result.IsFailure)
        {
            return result;
        }

        return Result.Success();
    }
}
