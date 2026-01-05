namespace CRMSystem.Core.DTOs.Work;

public record WorkItem
{
    public long Id { get; init; }
    public string Title { get; init; } = string.Empty;
    public string Categoty { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public decimal StandartTime { get; init; }
};
