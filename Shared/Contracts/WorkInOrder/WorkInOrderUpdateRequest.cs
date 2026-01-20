using Shared.Enums;

namespace Shared.Contracts.WorkInOrder;

public record WorkInOrderUpdateRequest
(
    int? WorkerId,
    WorkStatusEnum? StatusId,
    decimal? TimeSpent
);
