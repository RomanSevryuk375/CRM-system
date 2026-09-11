namespace CRM.Shared.Abstractions.Abstractions;

[System.Diagnostics.CodeAnalysis.SuppressMessage("Major Code Smell", "S3246:Generic type parameters should be co/contravariant when possible", Justification = "<Pending>")]
public interface IEntityId<T>
{
    T Id { get; }
}
