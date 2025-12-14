using CRMSystem.Core.Enums;

namespace CRMSystem.Core.DTOs.Bill;

public record BillItem
(
    long id,
    long orderId,
    string status,
    DateTime createdAt,
    decimal amount,
    DateOnly? actualBillDate
);

