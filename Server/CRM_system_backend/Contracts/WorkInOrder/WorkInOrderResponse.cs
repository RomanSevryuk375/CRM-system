namespace CRM_system_backend.Contracts.WorkInOrder;

public record WorkInOrderResponse
(
    long id,
    long orderId,
    string job,
    long jobId,
    string worker,
    int workerId,
    string status,
    int statusId,
    decimal timeSpent
);
