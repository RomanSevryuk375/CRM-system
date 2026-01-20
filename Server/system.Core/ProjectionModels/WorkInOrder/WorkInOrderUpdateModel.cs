using Shared.Enums;

namespace CRMSystem.Core.ProjectionModels.WorkInOrder;

public record WorkInOrderUpdateModel
(
    int? WorkerId,
    WorkStatusEnum? StatusId,
    decimal? TimeSpent
);
