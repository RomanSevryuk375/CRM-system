using CRM.Billing.Domain.Entities;
using CRM.Billing.Domain.Enums;
using CRM.Billing.Domain.Interfaces;
using CRM.Shared.Abstractions.Abstractions;
using CRM.Shared.Abstractions.Results;
using MassTransit;
using MediatR;

namespace CRM.Billing.Application.Features.Bills.Commands.CreateBill;

public sealed class CreateBillHandler(
    IBillRepository billRepository,
    TimeProvider timeProvider) : IRequestHandler<CreateBillCommand, Result>
{
    public async Task<Result> Handle(CreateBillCommand request, CancellationToken cancellationToken)
    {
        BillId billId = new(NewId.NextGuid());
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
            return Result.Failure(result.Error);
        }

        await billRepository.AddAsync(result.Value, cancellationToken);

        return Result.Success();
    }
}
