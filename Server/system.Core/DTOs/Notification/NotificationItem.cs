using CRMSystem.Core.Enums;

namespace CRMSystem.Core.DTOs.Notification;

public record NotificationItem
(
    long id, 
    string client, 
    string car, 
    string type, 
    string status, 
    string message, 
    DateTime sendAt
);

