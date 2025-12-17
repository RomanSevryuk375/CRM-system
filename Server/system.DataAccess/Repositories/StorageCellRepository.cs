using CRMSystem.Core.DTOs.StorageCell;
using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;

namespace CRMSystem.DataAccess.Repositories;

public class StorageCellRepository : IStorageCellRepository
{
    private readonly SystemDbContext _context;

    public StorageCellRepository(SystemDbContext context)
    {
        _context = context;
    }

    public async Task<List<StorageCellItem>> Get()
    {
        var quary = _context.StorageCells.AsNoTracking();

        var projection = quary.Select(s => new StorageCellItem(
            s.Id,
            s.Rack,
            s.Shelf));

        return await projection.ToListAsync();
    }

    public async Task<int> Create(StorageCell storageCell)
    {
        var storageCellEntity = new StorageCellEntity
        {
            Rack = storageCell.Rack,
            Shelf = storageCell.Shelf,
        };

        await _context.StorageCells.AddAsync(storageCellEntity);
        await _context.SaveChangesAsync();

        return storageCellEntity.Id;
    }

    public async Task<int> Update(int id, StorageCellUpdateModel model)
    {
        var entity = await _context.StorageCells.FirstOrDefaultAsync(s => s.Id == id)
            ?? throw new Exception("StorageCell not found");

        if (!string.IsNullOrWhiteSpace(model.rack)) entity.Rack = model.rack;
        if (!string.IsNullOrWhiteSpace(model.shelf)) entity.Shelf = model.shelf;

        await _context.SaveChangesAsync();

        return entity.Id;
    }

    public async Task<int> Delete(int id)
    {
        var entity = await _context.StorageCells
            .Where(s => s.Id == id)
            .ExecuteDeleteAsync();

        return id;
    }
}
