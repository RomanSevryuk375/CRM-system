using Shared.Enums;

namespace Shared.Contracts.Order;

public record OrderUpdateRequest
(
    OrderPriorityEnum PriorityId
);
