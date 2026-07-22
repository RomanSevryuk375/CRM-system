using CRM.Shared.Abstractions.Messaging;

namespace CRM.Shared.Abstractions.Abstractions;

public interface IHasDomainEvents
{
    IReadOnlyCollection<IDomainEvent> DomainEvents { get; }
    void ClearDomainEvents();
}
