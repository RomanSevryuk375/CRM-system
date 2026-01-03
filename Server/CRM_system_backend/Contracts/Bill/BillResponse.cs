namespace CRMSystem.Core.DTOs.Bill;

public record BillResponse
(
    long Id,
    long OrderId,
    string Status,
    int StatusId,
    DateTime CreatedAt,
    decimal Amount,
    DateOnly? ActualBillDate
);
