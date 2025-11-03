namespace CRM_system_backend.Contracts;

public record RepairNoteResponse
(
    int Id,
    int OrderId,
    int CarId,
    DateTime Date,
    decimal ServiceSum
);
