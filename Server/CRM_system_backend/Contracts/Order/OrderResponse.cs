namespace CRM_system_backend.Contracts.Order;

public record OrderResponse
(
    long id,
    string status,
    int statusId,
    string car,
    long carId,
    DateOnly date,
    string priority,
    int priorityId
);
