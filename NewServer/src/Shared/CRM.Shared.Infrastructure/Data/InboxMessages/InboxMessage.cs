namespace CRM.Shared.Infrastructure.Data.InboxMessages;

public sealed class InboxMessage
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public DateTime ProcessedOnUtc { get; init; }
}