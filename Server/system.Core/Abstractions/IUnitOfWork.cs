namespace CRMSystem.Core.Abstractions;

public interface IUnitOfWork
{
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackAsync();
}
