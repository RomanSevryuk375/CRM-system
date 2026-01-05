namespace CRM_system_backend.Contracts.Work;

public record WorkResponse
{
    public long Id { get; init; }
    public string Title { get; init; } = string.Empty;
    public string Categoty { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public decimal StandartTime { get; init; }
};
