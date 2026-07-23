using CRM.Shared.Abstractions.Abstractions;

namespace CRM.Shared.Abstractions.DDD;

public abstract class AggregateRoot<TId> : IEntity<TId>, IHasDomainEvents
{
    public TId Id { get; protected set; }

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