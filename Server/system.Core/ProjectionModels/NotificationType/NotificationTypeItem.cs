namespace CRMSystem.Core.ProjectionModels.NotificationType;

public record NotificationTypeItem
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
};
