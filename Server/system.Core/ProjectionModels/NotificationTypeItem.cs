namespace CRMSystem.Core.DTOs;

public record NotificationTypeItem
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
};
