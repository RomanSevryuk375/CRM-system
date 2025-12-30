namespace CRM_system_backend.Contracts.Bill;

public record BillResponse
(
    long id,
    long orderId,
    string status,
    int statusId,
    DateTime createdAt,
    decimal amount,
    DateOnly? actualBillDate
);
