using System;
using CRM.Ordering.Domain.Entities.Orders;
using CRM.Ordering.Domain.Enums;
using CRM.Shared.Abstractions.Abstractions;
using CRM.Shared.Abstractions.DDD.ValueObjects;

namespace CRM.TestHelpers;

public static class OrderWorkMother
{
    public static readonly OrderId DefaultOrderId = new(Guid.NewGuid());
    public static readonly JobId DefaultJobId = new(Guid.NewGuid());

    public static OrderWork CreatePending(
        OrderWorkId? id = null,
        OrderId? orderId = null,
        JobId? jobId = null,
        Money? hourlyRate = null,
        StandardHours? hours = null,
        Money? fixedPrice = null,
        bool isProposed = false)
    {
        return OrderWork.Create(
            id ?? new OrderWorkId(Guid.NewGuid()),
            orderId ?? DefaultOrderId,
            jobId ?? DefaultJobId,
            WorkStatus.Pending,
            hourlyRate ?? Money.Create(50m).Value,
            hours ?? StandardHours.Create(2m).Value,
            fixedPrice,
            isProposed).Value;
    }
}
