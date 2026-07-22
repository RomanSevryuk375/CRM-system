namespace CRM.Shared.Abstractions.Abstractions;

public interface IHasVersion
{
    Guid Version { get; }
}
