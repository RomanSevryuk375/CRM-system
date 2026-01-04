namespace CRMSystem.Core.DTOs;

public record NotificationStatusItem
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
};
