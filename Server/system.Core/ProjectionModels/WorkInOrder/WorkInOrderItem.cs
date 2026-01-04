using CRMSystem.Core.Enums;

namespace CRMSystem.Core.DTOs.WorkInOrder;

public record WorkInOrderItem
(
    long Id,
    long OrderId,
    string job,
    long JobId,
    string Worker,
    int WorkerId,
    string Status,
    int StatusId,
    decimal TimeSpent
);
