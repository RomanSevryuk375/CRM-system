using Shared.Enums;

namespace Shared.Contracts.Bill;

public record BillRequest
(
    long OrderId,
    string Status,
    BillStatusEnum StatusId,
    DateTime CreatedAt,
    decimal Amount,
    DateOnly? ActualBillDate
);
