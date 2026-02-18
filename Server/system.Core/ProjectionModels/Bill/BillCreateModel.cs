using Shared.Enums;

namespace CRMSystem.Core.ProjectionModels.Bill;

public record BillCreateModel
(
    long OrderId, 
    BillStatusEnum StatusId,
    DateTime CreatedAt,
    decimal Amount, 
    DateOnly? ActualBillDate);