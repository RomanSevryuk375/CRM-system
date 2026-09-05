namespace CRM.Shared.Abstractions.DDD;

using CRM.Shared.Abstractions.Abstractions;

public abstract class Entity<TId> : IEntity<TId>
{
    public TId Id { get; protected set; }
}
