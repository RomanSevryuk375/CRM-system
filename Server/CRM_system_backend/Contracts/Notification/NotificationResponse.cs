namespace CRM_system_backend.Contracts.Notification;

public record NotificationResponse
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
