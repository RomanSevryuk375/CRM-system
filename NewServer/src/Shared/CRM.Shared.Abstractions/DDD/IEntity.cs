namespace CRM.Shared.Abstractions.DDD;

public interface IEntity<out TId>
{
    TId Id { get; }
}