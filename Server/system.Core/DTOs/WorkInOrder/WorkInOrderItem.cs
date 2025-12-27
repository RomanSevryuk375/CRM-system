using CRMSystem.Core.Enums;

namespace CRMSystem.Core.DTOs.WorkInOrder;

public record WorkInOrderItem
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
