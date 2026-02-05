using CRMSystem.Core.Abstractions;
using Microsoft.EntityFrameworkCore.Storage;

namespace CRMSystem.DataAccess.Extensions;

public class UnitOfWork(SystemDbContext context) : IUnitOfWork
{
    private IDbContextTransaction? _currentTransaction;

    public async Task BeginTransactionAsync(CancellationToken ct)
    {
        _currentTransaction = await context.Database.BeginTransactionAsync(ct);
    }

    public async Task CommitTransactionAsync(CancellationToken ct)
    {
        try
        {
            await context.SaveChangesAsync(ct);
            if (_currentTransaction is not null) await _currentTransaction.CommitAsync(ct);
        }
        catch
        {
            await RollbackAsync(ct);
            throw;
        }
        finally
        {
            if (_currentTransaction is not null)
            {
                await _currentTransaction.DisposeAsync();
                _currentTransaction = null;
            }
        }
    }

    public async Task RollbackAsync(CancellationToken ct)
    {
        if (_currentTransaction is not null)
        {
            await _currentTransaction.RollbackAsync(ct);
            await _currentTransaction.DisposeAsync();
            _currentTransaction = null;
        }
    }

    public void Dispose()
    {
        context.Dispose();
        _currentTransaction?.Dispose();
        GC.SuppressFinalize(this);
    }
}
