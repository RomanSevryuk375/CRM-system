using CRMSystem.Core.Enums;

namespace CRMSystem.Core.DTOs.Bill;

public record BillUpdateModel
(
    BillStatusEnum? statusId,
    decimal? amount,
    DateOnly? actualBillDate
);
