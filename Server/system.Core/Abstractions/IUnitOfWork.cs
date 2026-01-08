namespace CRMSystem.Core.Abstractions;

public interface IUnitOfWork
{
    Task BeginTransactionAsync(CancellationToken ct);
    Task CommitTransactionAsync(CancellationToken ct);
    Task RollbackAsync(CancellationToken ct);
}
