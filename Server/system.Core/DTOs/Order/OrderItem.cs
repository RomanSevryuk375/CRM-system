using CRMSystem.Core.Enums;

namespace CRMSystem.Core.DTOs.Order;

public record OrderItem
(
    long id,
    string statusId, 
    string carId, 
    DateOnly date, 
    string priorityId
);
