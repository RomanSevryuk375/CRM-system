namespace CRM_system_backend.Contracts.WorkInOrder;

public record WorkInOrderResponse
(
    long Id,
    long OrderId,
    string Job,
    long JobId,
    string Worker,
    int WorkerId,
    string Status,
    int StatusId,
    decimal TimeSpent
);
