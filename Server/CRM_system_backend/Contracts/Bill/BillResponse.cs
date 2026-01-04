namespace CRMSystem.Core.DTOs.Bill;

public record BillResponse
{
    public long Id { get; init; }
    public long OrderIdv { get; init; }
    public string Status { get; init; } = string.Empty;
    public int StatusId { get; init; }
    public DateTime CreatedAt { get; init; }
    public decimal Amount { get; init; }
    public DateOnly? ActualBillDate { get; init; }
};
