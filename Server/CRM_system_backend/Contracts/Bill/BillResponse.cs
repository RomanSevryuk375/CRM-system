namespace CRM_system_backend.Contracts.Bill;

public record BillResponse
(
  int Id,
  int OrderId,
  int StatusId,
  DateTime Date,
  decimal Amount,
  DateTime? ActualBillDate
);
