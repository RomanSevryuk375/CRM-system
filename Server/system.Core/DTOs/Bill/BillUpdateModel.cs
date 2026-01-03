using CRMSystem.Core.Enums;

namespace CRMSystem.Core.DTOs.Bill;

public record BillUpdateModel
(
    BillStatusEnum? StatusId,
    decimal? Amount,
    DateOnly? ActualBillDate
);
