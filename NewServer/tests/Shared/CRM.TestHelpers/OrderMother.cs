using System;
using CRM.Ordering.Domain.Entities.Orders;
using CRM.Ordering.Domain.Enums;
using CRM.Shared.Abstractions.Abstractions;

namespace CRM.TestHelpers;

public static class OrderMother
{
    public static readonly DateOnly DefaultStartDate = new(2026, 9, 1);
    public static readonly DateOnly DefaultFinishDate = new(2026, 9, 15);
    public static readonly CarId DefaultCarId = new(Guid.NewGuid());
    public static readonly WorkerId DefaultWorkerId = new(Guid.NewGuid());

    public static Order CreatePending(
        OrderId? id = null,
        CarId? carId = null,
        WorkerId? workerId = null,
        DateOnly? startDate = null,
        DateOnly? finishDate = null,
        OrderPriority priority = OrderPriority.Medium)
    {
        return Order.Create(
            id ?? new OrderId(Guid.NewGuid()),
            carId ?? DefaultCarId,
            workerId ?? DefaultWorkerId,
            startDate ?? DefaultStartDate,
            finishDate ?? DefaultFinishDate,
            priority).Value;
    }
}
