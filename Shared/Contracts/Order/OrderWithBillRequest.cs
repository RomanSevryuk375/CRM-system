using Shared.Enums;

namespace Shared.Contracts.Order;

public record OrderWithBillRequest
(
    long Id,
    OrderStatusEnum OrderStatusId,
    long CarId,
    DateOnly Date,
    OrderPriorityEnum PriorityId,
    long OrderId,
    BillStatusEnum BillStatusId,
    DateTime CreatedAt,
    decimal Amount,
    DateOnly? ActualBillDate
);
