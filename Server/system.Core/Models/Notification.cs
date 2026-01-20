using CRMSystem.Core.Constants;
using CRMSystem.Core.Validation;
using Shared.Enums;

namespace CRMSystem.Core.Models;

public class Notification
{
    private Notification(long id, long clientId, long carId, NotificationTypeEnum typeId, NotificationStatusEnum statusId, string message, DateTime sendAt) 
    {
        Id = id;
        ClientId = clientId;
        CarId = carId;
        TypeId = typeId;
        StatusId = statusId;
        Message = message;
        SendAt = sendAt;
    }
    public long Id { get; }
    public long ClientId { get; }
    public long CarId { get; }
    public NotificationTypeEnum TypeId { get; }
    public string Message { get; } 
    public DateTime SendAt { get; }
    public NotificationStatusEnum StatusId { get; }

    public static (Notification? notification, List<string> errors) Create(long id, long clientId, long carId, NotificationTypeEnum typeId, NotificationStatusEnum statusId, string message, DateTime sendAt)
    {
        var errors = new List<string>();

        var idError = DomainValidator.ValidateId(id, "id");
        if (!string.IsNullOrEmpty(idError)) errors.Add(idError);

        var clientIdError = DomainValidator.ValidateId(clientId, "clientId");
        if (!string.IsNullOrEmpty(clientIdError)) errors.Add(clientIdError);

        var carIdError = DomainValidator.ValidateId(carId, "carId");
        if (!string.IsNullOrEmpty(carIdError)) errors.Add(carIdError);

        var statusIdError = DomainValidator.ValidateId(statusId, "statusId");
        if (!string.IsNullOrEmpty(statusIdError)) errors.Add(statusIdError);

        var typeIdError = DomainValidator.ValidateId(typeId, "typeId");
        if (!string.IsNullOrEmpty(typeIdError)) errors.Add(typeIdError); 

        var messageError = DomainValidator.ValidateString(message, ValidationConstants.MAX_DESCRIPTION_LENGTH, "message");
        if (!string.IsNullOrEmpty(messageError)) errors.Add(messageError);

        var sendAtError = DomainValidator.ValidateDate(sendAt, "sendAt");
        if (!string.IsNullOrEmpty(sendAtError)) errors.Add(sendAtError);

        if (errors.Any())
            return (null, errors);

        var notification = new Notification(id, clientId, carId, typeId, statusId, message, sendAt);

        return (notification, new List<string>());
    }
}
