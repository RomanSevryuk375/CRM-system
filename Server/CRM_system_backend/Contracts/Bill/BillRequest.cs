using CRMSystem.Core.Enums;

namespace CRM_system_backend.Contracts.Bill;

public record BillRequest
(
    long OrderId,
    string Status,
    BillStatusEnum StatusId,
    DateTime CreatedAt,
    decimal Amount,
    DateOnly? ActualBillDate
);
