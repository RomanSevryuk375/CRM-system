namespace CRM_system_backend.Contracts;

public record OrderResponse
(
    int Id,
    int StatusId,
    int CarId,
    DateTime Date,
    string Priority
);
