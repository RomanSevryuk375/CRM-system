using Shared.Enums;

namespace Shared.Contracts.Order;

public record OrderPatchRequest
{
    public OrderStatusEnum OrderStatus { get; init; }
}