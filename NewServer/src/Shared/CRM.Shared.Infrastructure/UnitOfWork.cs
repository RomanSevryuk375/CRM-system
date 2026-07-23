using CRM.Shared.Abstractions.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace CRM.Shared.Infrastructure;

public sealed class UnitOfWork<TDbContext>(TDbContext dbContext)
    : IUnitOfWork where TDbContext : DbContext
{
    private IDbContextTransaction? _contextTransaction;
    public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        _contextTransaction =
            await dbContext.Database.BeginTransactionAsync(cancellationToken);
    }

    public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            await dbContext.SaveChangesAsync(cancellationToken);
            if (_contextTransaction is not null)
            {
                await _contextTransaction.CommitAsync(cancellationToken);
            }
        }
        catch
        {
            await RollbackTransactionAsync(cancellationToken);
            throw;
        }
        finally
        {
            if (_contextTransaction is not null)
            {
                await _contextTransaction.DisposeAsync();
                _contextTransaction = null;
            }
        }
    }

    public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_contextTransaction is not null)
        {
            await _contextTransaction.RollbackAsync(cancellationToken);
            await _contextTransaction.DisposeAsync();
            _contextTransaction = null;
        }
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
