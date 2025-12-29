namespace CRM_system_backend.Contracts.Bill;

public record BillRequest
(
     int OrderId, 
     int StatusId, 
     DateTime Date
);
