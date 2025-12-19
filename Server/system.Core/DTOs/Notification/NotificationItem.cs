using CRMSystem.Core.Enums;

namespace CRMSystem.Core.DTOs.Notification;

public record NotificationItem
(
    long id, 
    string client,
    long clientId,
    string car, 
    long carId,
    string type, 
    int typeId,
    string status, 
    int statusId,
    string message, 
    DateTime sendAt
);

