using AutoMapper;
using AutoMapper.QueryableExtensions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels.StorageCell;
using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;

namespace CRMSystem.DataAccess.Repositories;

public class StorageCellRepository : IStorageCellRepository
{
    private readonly SystemDbContext _context;
    private readonly IMapper _mapper;

    public StorageCellRepository(
        SystemDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<StorageCellItem>> Get(CancellationToken ct)
    {
        return await _context.StorageCells
            .AsNoTracking()
            .ProjectTo<StorageCellItem>(_mapper.ConfigurationProvider, ct)
            .ToListAsync(ct);
    }

    public async Task<int> Create(StorageCell storageCell, CancellationToken ct)
    {
        var storageCellEntity = new StorageCellEntity
        {
            Rack = storageCell.Rack,
            Shelf = storageCell.Shelf,
        };

        await _context.StorageCells.AddAsync(storageCellEntity, ct);
        await _context.SaveChangesAsync(ct);

        return storageCellEntity.Id;
    }

    public async Task<int> Update(int id, StorageCellUpdateModel model, CancellationToken ct)
    {
        var entity = await _context.StorageCells.FirstOrDefaultAsync(s => s.Id == id, ct)
            ?? throw new Exception("StorageCell not found");

        if (!string.IsNullOrWhiteSpace(model.Rack)) entity.Rack = model.Rack;
        if (!string.IsNullOrWhiteSpace(model.Shelf)) entity.Shelf = model.Shelf;

        await _context.SaveChangesAsync(ct);

        return entity.Id;
    }

    public async Task<int> Delete(int id, CancellationToken ct)
    {
        var entity = await _context.StorageCells
            .Where(s => s.Id == id)
            .ExecuteDeleteAsync(ct);

        return id;
    }

    public async Task<bool> Exists(int id, CancellationToken ct)
    {
        return await _context.StorageCells
            .AsNoTracking()
            .AnyAsync(p => p.Id == id, ct);
    }

    public async Task<bool> HasOverlaps(string rack, string shelf, CancellationToken ct)
    {
        return await _context.StorageCells
            .AsNoTracking()
            .AnyAsync(s => (s.Rack == rack && s.Shelf == shelf), ct);
    }
}
