namespace Shared.Contracts;

public record NotificationTypeResponse
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
};
