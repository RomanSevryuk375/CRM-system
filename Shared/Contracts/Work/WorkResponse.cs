namespace Shared.Contracts.Work;

public record WorkResponse
{
    public long Id { get; init; }
    public string Title { get; init; } = string.Empty;
    public string Category { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public decimal StandardTime { get; init; }
};
