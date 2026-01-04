namespace CRMSystem.Core.DTOs.Guarantee;

public record GuaranteeItem
{
    public long Id { get; init; }
    public long OrderId { get; init; }
    public DateOnly DateStart { get; init; }
    public DateOnly DateEnd { get; init; }
    public string? Description { get; init; }
    public string Terms { get; init; } = string.Empty;
};
