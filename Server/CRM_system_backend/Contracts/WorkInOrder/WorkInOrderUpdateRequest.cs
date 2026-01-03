using CRMSystem.Core.Enums;

namespace CRM_system_backend.Contracts.WorkInOrder;

public record WorkInOrderUpdateRequest
(
    int? WorkerId,
    WorkStatusEnum? StatusId,
    decimal? TimeSpent
);
