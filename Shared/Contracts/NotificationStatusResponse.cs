namespace Shared.Contracts;

public record NotificationStatusResponse
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
};
