using CRMSystem.Core.Enums;

namespace CRM_system_backend.Contracts.Order;

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
