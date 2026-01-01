using CRMSystem.Core.Enums;

namespace CRM_system_backend.Contracts.WorkInOrder;

public record WorkInOrderUpdateRequest
(
    int? workerId,
    WorkStatusEnum? statusId,
    decimal? timeSpent
);
