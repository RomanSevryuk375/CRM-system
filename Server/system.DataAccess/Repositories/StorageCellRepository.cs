using AutoMapper;
using AutoMapper.QueryableExtensions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels.StorageCell;
using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;
using CRMSystem.Core.Exceptions;

namespace CRMSystem.DataAccess.Repositories;

public class StorageCellRepository(
    SystemDbContext context,
    IMapper mapper) : IStorageCellRepository
{
    public async Task<List<StorageCellItem>> Get(CancellationToken ct)
    {
        return await context.StorageCells
            .AsNoTracking()
            .ProjectTo<StorageCellItem>(mapper.ConfigurationProvider, ct)
            .ToListAsync(ct);
    }

    public async Task<int> Create(StorageCell storageCell, CancellationToken ct)
    {
        var storageCellEntity = new StorageCellEntity
        {
            Rack = storageCell.Rack,
            Shelf = storageCell.Shelf,
        };

        await context.StorageCells.AddAsync(storageCellEntity, ct);
        await context.SaveChangesAsync(ct);

        return storageCellEntity.Id;
    }

    public async Task<int> Update(int id, StorageCellUpdateModel model, CancellationToken ct)
    {
        var entity = await context.StorageCells.FirstOrDefaultAsync(s => s.Id == id, ct)
            ?? throw new NotFoundException("StorageCell not found");

        if (!string.IsNullOrWhiteSpace(model.Rack))
        {
            entity.Rack = model.Rack;
        }

        if (!string.IsNullOrWhiteSpace(model.Shelf))
        {
            entity.Shelf = model.Shelf;
        }

        await context.SaveChangesAsync(ct);

        return entity.Id;
    }

    public async Task<int> Delete(int id, CancellationToken ct)
    {
        await context.StorageCells
            .Where(s => s.Id == id)
            .ExecuteDeleteAsync(ct);

        return id;
    }

    public async Task<bool> Exists(int id, CancellationToken ct)
    {
        return await context.StorageCells
            .AsNoTracking()
            .AnyAsync(p => p.Id == id, ct);
    }

    public async Task<bool> HasOverlaps(string rack, string shelf, CancellationToken ct)
    {
        return await context.StorageCells
            .AsNoTracking()
            .AnyAsync(s => (s.Rack == rack && s.Shelf == shelf), ct);
    }
}
