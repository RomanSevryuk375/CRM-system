using CRMSystem.Core.Enums;

namespace CRMSystem.Core.DTOs.WorkInOrder;

public record WorkInOrderItem
{
    public long Id { get; init; }
    public long OrderId { get; init; }
    public string job { get; init; } = string.Empty;
    public long JobId { get; init; }
    public string Worker { get; init; } = string.Empty;
    public int WorkerId { get; init; }
    public string Status { get; init; } = string.Empty;
    public int StatusId { get; init; }
    public decimal TimeSpent { get; init; }
};
