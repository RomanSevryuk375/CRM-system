using CRMSystem.Core.Enums;

namespace CRM_system_backend.Contracts.WorkInOrder;

public record WorkInOrderRequest
(
    long id,
    long orderId,
    long jobId,
    int workerId,
    WorkStatusEnum statusId,
    decimal timeSpent
);
