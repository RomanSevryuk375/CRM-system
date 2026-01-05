using AutoMapper;
using AutoMapper.QueryableExtensions;
using CRMSystem.Core.DTOs.StorageCell;
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

    public async Task<List<StorageCellItem>> Get()
    {
        return await _context.StorageCells
            .AsNoTracking()
            .ProjectTo<StorageCellItem>(_mapper.ConfigurationProvider)
            .ToListAsync();
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

        if (!string.IsNullOrWhiteSpace(model.Rack)) entity.Rack = model.Rack;
        if (!string.IsNullOrWhiteSpace(model.Shelf)) entity.Shelf = model.Shelf;

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

    public async Task<bool> Exists(int id)
    {
        return await _context.StorageCells
            .AsNoTracking()
            .AnyAsync(p => p.Id == id);
    }

    public async Task<bool> HasOverlaps(string rack, string shelf)
    {
        return await _context.StorageCells
            .AsNoTracking()
            .AnyAsync(s => (s.Rack == rack && s.Shelf == shelf));
    }
}
