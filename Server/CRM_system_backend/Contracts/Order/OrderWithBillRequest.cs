using CRMSystem.Core.Enums;

namespace CRM_system_backend.Contracts.Order;

public record OrderWithBillRequest
(
    long id,
    OrderStatusEnum orderStatusId, 
    long carId, 
    DateOnly date, 
    OrderPriorityEnum priorityId, 
    long orderId, 
    BillStatusEnum billStatusId, 
    DateTime createdAt, 
    decimal amount,
    DateOnly? actualBillDate
);
