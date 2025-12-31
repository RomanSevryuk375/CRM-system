using CRMSystem.Core.Enums;

namespace CRM_system_backend.Contracts.Order;

public record OrderRequest
(
    long id,
    OrderStatusEnum statusId,
    long carId,
    DateOnly date,
    OrderPriorityEnum priorityId
);
