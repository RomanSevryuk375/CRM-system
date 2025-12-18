using CRMSystem.Core.Enums;

namespace CRMSystem.Core.DTOs.WorkInOrder;

public record WorkInOrderUpdateModel
(
    int? workerId,
    WorkStatusEnum? statusId,
    decimal? timeSpent
);
