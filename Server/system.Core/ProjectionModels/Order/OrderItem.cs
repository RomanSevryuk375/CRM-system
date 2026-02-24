namespace CRMSystem.Core.ProjectionModels.Order;

public record OrderItem
{
    public long Id { get; init; }
    public string Status { get; init; } = string.Empty;
    public int StatusId { get; init; }
    public string Car { get; init; } = string.Empty;
    public long CarId { get; init; }
    public DateOnly Date { get; init; }
    
    public string? OrderPdfFileName { get; set; } = string.Empty;
    public string? OrderAgreementPdfFileName { get; set; } = string.Empty;
    public string Priority { get; init; } = string.Empty;
    public int PriorityId { get; init; }
};
