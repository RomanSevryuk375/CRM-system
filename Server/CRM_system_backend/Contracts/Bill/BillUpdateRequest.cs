using CRMSystem.Core.Enums;

namespace CRM_system_backend.Contracts.Bill;

public record BillUpdateRequest
(
    BillStatusEnum? statusId,
    decimal? amount,
    DateOnly? actualBillDate
);
