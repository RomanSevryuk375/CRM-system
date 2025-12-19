using CRMSystem.Core.DTOs.SupplySet;
using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;

namespace CRMSystem.DataAccess.Repositories;

public class SupplySetRepository : ISupplySetRepository
{
    private readonly SystemDbContext _context;

    public SupplySetRepository(SystemDbContext context)
    {
        _context = context;
    }

    private IQueryable<SupplySetEntity> ApplyFilter(IQueryable<SupplySetEntity> query, SupplySetFilter filter)
    {
        if (filter.supplyIds != null && filter.supplyIds.Any())
            query = query.Where(s => filter.supplyIds.Contains(s.SupplyId));

        if (filter.positionIds != null && filter.positionIds.Any())
            query = query.Where(s => filter.positionIds.Contains(s.PositionId));

        return query;
    }

    public async Task<List<SupplySetItem>> GetPaged(SupplySetFilter filter)
    {
        var query = _context.SupplySets.AsNoTracking();
        query = ApplyFilter(query, filter);

        query = filter.SortBy?.ToLower().Trim() switch
        {
            "supply" => filter.isDescending
                ? query.OrderByDescending(s => s.SupplyId)
                : query.OrderBy(s => s.SupplyId),
            "position" => filter.isDescending
                ? query.OrderByDescending(s => s.Position == null
                    ? string.Empty
                    : s.Position.Part == null
                        ? string.Empty
                        : s.Position.Part.Name)
                : query.OrderBy(s => s.Position == null
                    ? string.Empty
                    : s.Position.Part == null
                        ? string.Empty
                        : s.Position.Part.Name),
            "quantity" => filter.isDescending
                ? query.OrderByDescending(s => s.Quantity)
                : query.OrderBy(s => s.Quantity),
            "purchaseprice" => filter.isDescending
                ? query.OrderByDescending(s => s.PurchasePrice)
                : query.OrderBy(s => s.PurchasePrice),

            _ => filter.isDescending
                ? query.OrderByDescending(s => s.Id)
                : query.OrderBy(s => s.Id),
        };

        var projection = query.Select(s => new SupplySetItem(
            s.Id,
            s.SupplyId,
            s.Position == null
                    ? string.Empty
                    : s.Position.Part == null
                        ? string.Empty
                        : s.Position.Part.Name,
            s.PositionId,
            s.Quantity,
            s.PurchasePrice));

        return await projection
            .Skip((filter.Page - 1) * filter.Limit)
            .Take(filter.Limit)
            .ToListAsync();
    }
    public async Task<int> GetCount(SupplySetFilter filter)
    {
        var query = _context.SupplySets.AsNoTracking();
        query = ApplyFilter(query, filter);
        return await query.CountAsync();
    }

    public async Task<long> Create(SupplySet supplySet)
    {
        var supplySetEntity = new SupplySetEntity
        {
            SupplyId = supplySet.SupplyId,
            PositionId = supplySet.PositionId,
            Quantity = supplySet.Quantity,
            PurchasePrice = supplySet.PurchasePrice,
        };

        await _context.SupplySets.AddAsync(supplySetEntity);
        await _context.SaveChangesAsync();

        return supplySetEntity.Id;
    }

    public async Task<long> Update(long id, SupplySetUpdateModel model)
    {
        var entity = await _context.SupplySets.FirstOrDefaultAsync(s => s.Id == id)
            ?? throw new Exception("SupplySet note not found");

        if (model.quantity.HasValue) entity.Quantity = model.quantity.Value;
        if (model.purchasePrice.HasValue) entity.PurchasePrice = model.purchasePrice.Value;

        await _context.SaveChangesAsync();

        return entity.Id;
    }

    public async Task<long> Delete(long id)
    {
        var entity = await _context.SupplySets
            .Where(s => s.Id == id)
            .ExecuteDeleteAsync();

        return id;
    }
}
