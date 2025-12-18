using CRMSystem.Core.Enums;

namespace CRMSystem.Core.DTOs.WorkInOrder;

public record WorkInOrderItem
(
    long id, 
    long orderId, 
    string job, 
    string worker, 
    string status, 
    decimal timeSpent
);
