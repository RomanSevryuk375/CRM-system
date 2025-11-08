namespace CRM_system_backend.Contracts;

public record BillRequest
(
     int OrderId, 
     int StatusId, 
     DateTime Date
);
