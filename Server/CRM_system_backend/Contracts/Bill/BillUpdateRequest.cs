using CRMSystem.Core.Enums;

namespace CRMSystem.Core.DTOs.Bill;

public record BillUpdateRequest
(
    BillStatusEnum? StatusId,
    decimal? Amount,
    DateOnly? ActualBillDate
);
