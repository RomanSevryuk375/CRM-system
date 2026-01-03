using CRMSystem.Core.Enums;

namespace CRMSystem.Core.DTOs.Bill;

public record BillItem
(
    long Id,
    long OrderId,
    string Status,
    int StatusId,
    DateTime CreatedAt,
    decimal Amount,
    DateOnly? ActualBillDate
);

