using CRMSystem.Core.Enums;

namespace CRMSystem.Core.DTOs.Bill;

public record BillItem
(
    long id,
    long orderId,
    string status,
    int statusId,
    DateTime createdAt,
    decimal amount,
    DateOnly? actualBillDate
);

