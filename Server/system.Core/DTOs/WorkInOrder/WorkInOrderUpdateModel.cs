using CRMSystem.Core.Enums;

namespace CRMSystem.Core.DTOs.WorkInOrder;

public record WorkInOrderUpdateModel
(
    int? WorkerId,
    WorkStatusEnum? StatusId,
    decimal? TimeSpent
);
