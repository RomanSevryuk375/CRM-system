using CRMSystem.Core.DTOs.Position;
using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;

namespace CRMSystem.DataAccess.Repositories;

public class PositionRepository : IPositionRepository
{
    private readonly SystemDbContext _context;

    public PositionRepository(SystemDbContext context)
    {
        _context = context;
    }

    private IQueryable<PositionEntity> ApplyFilter(IQueryable<PositionEntity> query, PositionFilter filter)
    {
        if (filter.partIds != null && filter.partIds.Any())
            query = query.Where(p => filter.partIds.Contains(p.PartId));

        return query;
    }

    public async Task<List<PositionItem>> GetPaged(PositionFilter filter)
    {
        var query = _context.Positions.AsNoTracking();
        query = ApplyFilter(query, filter);

        query = filter.SortBy?.ToLower().Trim() switch
        {
            "part" => filter.isDescending
                ? query.OrderByDescending(p => p.Part == null
                    ? string.Empty
                    : p.Part.Name)
                : query.OrderBy(p => p.Part == null
                    ? string.Empty
                    : p.Part.Name),
            "purchaseprice" => filter.isDescending
                ? query.OrderByDescending(p => p.PurchasePrice)
                : query.OrderBy(p => p.PurchasePrice),
            "sellingprice" => filter.isDescending
                ? query.OrderByDescending(p => p.SellingPrice)
                : query.OrderBy(p => p.SellingPrice),
            "quantity" => filter.isDescending
                ? query.OrderByDescending(p => p.Quantity)
                : query.OrderBy(p => p.Quantity),
            "cell" => filter.isDescending
                ? query.OrderByDescending(p => p.CellId)
                : query.OrderBy(p => p.CellId),

            _ => filter.isDescending
                ? query.OrderByDescending(p => p.Id)
                : query.OrderBy(p => p.Id),
        };

        var projection = query.Select(p => new PositionItem(
            p.Id,
            p.Part == null
                ? string.Empty
                : p.Part.Name,
            p.PartId,
            p.CellId,
            p.PurchasePrice,
            p.SellingPrice,
            p.Quantity));

        return await projection
            .Skip((filter.Page - 1) * filter.Limit)
            .Take(filter.Limit)
            .ToListAsync();
    }

    public async Task<int> GetCount(PositionFilter filter)
    {
        var query = _context.Positions.AsNoTracking();
        query = ApplyFilter(query, filter);
        return await query.CountAsync();
    }

    public async Task<long> Create(Position position)
    {
        var positionentity = new PositionEntity
        {
            PartId = position.PartId,
            CellId = position.CellId,
            PurchasePrice = position.PurchasePrice,
            SellingPrice = position.SellingPrice,
            Quantity = position.Quantity
        };

        await _context.AddAsync(positionentity);
        await _context.SaveChangesAsync();

        return positionentity.Id;
    }

    public async Task<long> Update(long id, PositionUpdateModel model)
    {
        var entity = await _context.Positions.FirstOrDefaultAsync(p => p.Id == id)
            ?? throw new Exception("Position note not found");

        if (model.cellId.HasValue) entity.CellId = model.cellId.Value;
        if (model.purchasePrice.HasValue) entity.PurchasePrice = model.purchasePrice.Value;
        if (model.sellingPrice.HasValue) entity.SellingPrice = model.sellingPrice.Value;
        if (model.quantity.HasValue) entity.Quantity = model.quantity.Value;

        await _context.SaveChangesAsync();

        return entity.Id;
    }

    public async Task<long> Delete(long id)
    {
        var entity = await _context.Positions
            .Where(p => p.Id == id)
            .ExecuteDeleteAsync();

        return id;
    }

    public async Task<bool> Exists(long id)
    {
        return await _context.Positions
            .AsNoTracking()
            .AnyAsync(p => p.Id == id);
    }
} 
