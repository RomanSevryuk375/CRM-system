using CRMSystem.Core.Enums;

namespace CRMSystem.Core.DTOs.Notification;

public record NotificationItem
{
    public long Id { get; init; }
    public string Client { get; init; } = string.Empty;
    public long ClientId { get; init; }
    public string Car { get; init; } = string.Empty;
    public long CarId { get; init; }
    public string Type { get; init; } = string.Empty;
    public int TypeId { get; init; }
    public string Status { get; init; } = string.Empty;
    public int StatusId { get; init; }
    public string Message { get; init; } = string.Empty;
    public DateTime SendAt { get; init; }
};

