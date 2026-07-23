using CRM.Shared.Abstractions.DDD;

namespace CRM.Shared.Abstractions.Abstractions;

public interface IHasDomainEvents
{
    IReadOnlyCollection<IDomainEvent> DomainEvents { get; }
    void ClearDomainEvents();
}
