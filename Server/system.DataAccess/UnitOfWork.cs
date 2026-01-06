using CRMSystem.Core.Abstractions;
using Microsoft.EntityFrameworkCore.Storage;

namespace CRMSystem.DataAccess;

public class UnitOfWork : IUnitOfWork
{
    private readonly SystemDbContext _context;
    private IDbContextTransaction? _currentTransaction;

    public UnitOfWork(SystemDbContext context) => _context = context;

    public async Task BeginTransactionAsync()
    {
        _currentTransaction = await _context.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        try
        {
            await _context.SaveChangesAsync();
            if (_currentTransaction is not null) await _currentTransaction.CommitAsync();
        }
        catch
        {
            await RollbackAsync();
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

    public async Task RollbackAsync()
    {
        if (_currentTransaction is not null)
        {
            await _currentTransaction.RollbackAsync();
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
