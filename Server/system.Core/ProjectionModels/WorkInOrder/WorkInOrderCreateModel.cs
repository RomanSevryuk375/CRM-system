using Shared.Enums;

namespace CRMSystem.Core.ProjectionModels.WorkInOrder;

public record WorkInOrderCreateModel
(
    long OrderId,
    long JobId,
    int WorkerId,
    WorkStatusEnum StatusId,
    decimal TimeSpent);