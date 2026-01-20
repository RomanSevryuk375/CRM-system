using Shared.Enums;

namespace CRMSystem.Core.ProjectionModels.Bill;

public record BillUpdateModel
{
    public BillStatusEnum? StatusId { get; init; }
    public decimal? Amount { get; init; }
    public DateOnly? ActualBillDate { get; init; }
};
