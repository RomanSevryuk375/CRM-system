using Shared.Enums;

namespace Shared.Contracts.Order;

public record OrderRequest
(
    long Id,
    OrderStatusEnum StatusId,
    long CarId,
    DateOnly Date,
    OrderPriorityEnum PriorityId
);
