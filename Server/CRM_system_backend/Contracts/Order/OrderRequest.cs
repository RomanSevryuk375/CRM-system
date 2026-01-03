using CRMSystem.Core.Enums;

namespace CRM_system_backend.Contracts.Order;

public record OrderRequest
(
    long Id,
    OrderStatusEnum StatusId,
    long CarId,
    DateOnly Date,
    OrderPriorityEnum PriorityId
);
