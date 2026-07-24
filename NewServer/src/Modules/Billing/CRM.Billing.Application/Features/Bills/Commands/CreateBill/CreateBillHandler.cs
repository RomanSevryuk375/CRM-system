using CRM.Billing.Domain.Entities;
using CRM.Billing.Domain.Enums;
using CRM.Billing.Domain.Interfaces;
using CRM.Shared.Abstractions.Abstractions;
using CRM.Shared.Abstractions.CQRS;
using CRM.Shared.Abstractions.Results;

namespace CRM.Billing.Application.Features.Bills.Commands.CreateBill;

public sealed class CreateBillHandler(
    IBillRepository billRepository,
    TimeProvider timeProvider)
    : ICommandHandler<CreateBillCommand>
{
    public async Task<Result> Handle(CreateBillCommand request, CancellationToken cancellationToken)
    {
        BillId billId = new(request.BillId);
        OrderId orderId = new(request.OrderId);
        BillStatus status = (BillStatus)request.StatusId;
        DateTimeOffset today = timeProvider.GetUtcNow();

        Result<Bill> result = Bill.Create(
            billId,
            orderId,
            status,
            request.Amount,
            request.ActualBillDate,
            DateOnly.FromDateTime(today.Date));
        if (result.IsFailure)
        {
            return result;
        }

        await billRepository.AddAsync(result.Value, cancellationToken);

        return Result.Success();
    }
}
