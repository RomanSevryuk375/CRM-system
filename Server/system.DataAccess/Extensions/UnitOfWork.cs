using CRMSystem.Core.Abstractions;
using Microsoft.EntityFrameworkCore.Storage;

namespace CRMSystem.DataAccess.Extensions;

public class UnitOfWork : IUnitOfWork
{
    private readonly SystemDbContext _context;
    private IDbContextTransaction? _currentTransaction;

    public UnitOfWork(SystemDbContext context) => _context = context;

    public async Task BeginTransactionAsync(CancellationToken ct)
    {
        _currentTransaction = await _context.Database.BeginTransactionAsync(ct);
    }

    public async Task CommitTransactionAsync(CancellationToken ct)
    {
        try
        {
            await _context.SaveChangesAsync(ct);
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
        _context.Dispose();
        _currentTransaction?.Dispose();
    }
}
