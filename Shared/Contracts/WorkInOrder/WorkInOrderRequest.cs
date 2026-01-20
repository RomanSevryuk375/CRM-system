using Shared.Enums;

namespace Shared.Contracts.WorkInOrder;

public record WorkInOrderRequest
(
    long Id,
    long OrderId,
    long JobId,
    int WorkerId,
    WorkStatusEnum StatusId,
    decimal TimeSpent
);
