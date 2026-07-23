using MediatR;

namespace CRM.Shared.Abstractions.DDD;

public interface IDomainEvent : INotification
{
    Guid EventId { get; }
    DateTime OccurredOn { get; }
}