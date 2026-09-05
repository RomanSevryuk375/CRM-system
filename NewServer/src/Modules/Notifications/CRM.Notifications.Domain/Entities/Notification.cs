using CRM.Notifications.Domain.Enums;
using CRM.Shared.Abstractions.Abstractions;
using CRM.Shared.Abstractions.DDD;
using CRM.Shared.Abstractions.Results;

namespace CRM.Notifications.Domain.Entities;

public sealed class Notification : AggregateRoot<NotificationId>, IAuditable, ISoftDeletable
{
    private Notification(
        NotificationId id,
        CustomerId customerId,
        CarId? carId,
        NotificationType type,
        string message,
        DateTime sendAt)
    {
        Id = id;
        CustomerId = customerId;
        CarId = carId;
        Type = type;
        Status = NotificationStatus.Pending;
        Message = message;
        SendAt = sendAt;
    }

#pragma warning disable CS8618
    private Notification() { }
#pragma warning restore CS8618

    public CustomerId CustomerId { get; private set; }
    public CarId? CarId { get; private set; }
    public NotificationType Type { get; private set; }
    public NotificationStatus Status { get; private set; }
    public string Message { get; private set; }
    public DateTime SendAt { get; private set; }

#pragma warning disable S1144
    public bool IsDeleted { get; private set; }
    public DateTimeOffset? DeletedAt { get; private set; }
    public Guid? DeletedBy { get; private set; }

    public DateTimeOffset CreatedAt { get; private set; }
    public Guid CreatedBy { get; private set; }
    public DateTimeOffset? UpdatedAt { get; private set; }
    public Guid? UpdatedBy { get; private set; }
#pragma warning restore S1144

    public static Result<Notification> Create(
        NotificationId id,
        CustomerId customerId,
        CarId? carId,
        NotificationType type,
        string message,
        DateTime sendAt)
    {
        if (string.IsNullOrWhiteSpace(message))
        {
            return Result<Notification>.Failure(Error.Validation<Notification>(Errors.MessageEmpty));
        }

        Notification notification = new(id, customerId, carId, type, message, sendAt);
        notification.IncrementVersion();

        return Result<Notification>.Success(notification);
    }

    public Result ChangeStatus(NotificationStatus newStatus)
    {
        if (Status == newStatus)
        {
            return Result.Success();
        }

        Status = newStatus;
        IncrementVersion();

        return Result.Success();
    }

    public static class Errors
    {
        public const string MessageEmpty = "Message cannot be empty.";
    }
}
