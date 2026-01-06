using CRMSystem.Core.Enums;

namespace CRM_system_backend.Contracts.Bill;

public record BillUpdateRequest
{
    public BillStatusEnum? StatusId { get; init; }
    public decimal? Amount { get; init; }
    public DateOnly? ActualBillDate { get; init; }
};
