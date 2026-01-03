using CRMSystem.Core.Enums;

namespace CRM_system_backend.Contracts.WorkInOrder;

public record WorkInOrderRequest
(
    long Id,
    long OrderId,
    long JobId,
    int WorkerId,
    WorkStatusEnum StatusId,
    decimal TimeSpent
);
