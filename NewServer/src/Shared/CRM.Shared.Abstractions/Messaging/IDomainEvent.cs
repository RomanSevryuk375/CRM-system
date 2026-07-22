using MediatR;

namespace CRM.Shared.Abstractions.Messaging;

public interface IDomainEvent : INotification
{
    DateTime OccurredOn { get; }
}
