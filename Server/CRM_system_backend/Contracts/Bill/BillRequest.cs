using CRMSystem.Core.Enums;

namespace CRM_system_backend.Contracts.Bill;

public record BillRequest
(
    long orderId,
    string status,
    BillStatusEnum statusId,
    DateTime createdAt,
    decimal amount,
    DateOnly? actualBillDate
);
