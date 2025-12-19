using CRMSystem.Core.Enums;

namespace CRMSystem.Core.DTOs.Order;

public record OrderItem
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
