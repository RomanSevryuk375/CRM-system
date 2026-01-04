using CRMSystem.Core.Enums;

namespace CRMSystem.Core.DTOs.Bill;

public record BillUpdateRequest
{
    public BillStatusEnum? StatusId { get; init; }
    public decimal? Amount { get; init; }
    public DateOnly? ActualBillDate { get; init; }
};
