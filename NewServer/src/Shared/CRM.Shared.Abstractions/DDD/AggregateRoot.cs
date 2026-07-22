using CRM.Shared.Abstractions.Abstractions;
using CRM.Shared.Abstractions.Messaging;

namespace CRM.Shared.Abstractions.DDD;

public abstract class AggregateRoot : IEntity, IHasDomainEvents
{
    public Guid Id { get; protected set; }

    private readonly List<IDomainEvent> _domainEvents = [];
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    protected void RaiseDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}