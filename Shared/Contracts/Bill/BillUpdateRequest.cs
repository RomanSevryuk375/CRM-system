using Shared.Enums;

namespace Shared.Contracts.Bill;

public record BillUpdateRequest
{
    public BillStatusEnum? StatusId { get; init; }
    public decimal? Amount { get; init; }
    public DateOnly? ActualBillDate { get; init; }
};
