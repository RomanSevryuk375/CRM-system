using AutoMapper;
using AutoMapper.QueryableExtensions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels.SupplySet;
using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;
using CRMSystem.Core.Exceptions;
using Shared.Filters;

namespace CRMSystem.DataAccess.Repositories;

public class SupplySetRepository(
    SystemDbContext context,
    IMapper mapper) : ISupplySetRepository
{
    private static IQueryable<SupplySetEntity> ApplyFilter(IQueryable<SupplySetEntity> query, SupplySetFilter filter)
    {
        if (filter.SupplyIds != null && filter.SupplyIds.Any())
        {
            query = query.Where(s => filter.SupplyIds.Contains(s.SupplyId));
        }

        if (filter.PositionIds != null && filter.PositionIds.Any())
        {
            query = query.Where(s => filter.PositionIds.Contains(s.PositionId));
        }

        return query;
    }

    public async Task<List<SupplySetItem>> GetPaged(SupplySetFilter filter, CancellationToken ct)
    {
        var query = context.SupplySets.AsNoTracking();
        query = ApplyFilter(query, filter);

        query = filter.SortBy?.ToLower().Trim() switch
        {
            "supply" => filter.IsDescending
                ? query.OrderByDescending(s => s.SupplyId)
                : query.OrderBy(s => s.SupplyId),

            "position" => filter.IsDescending
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

            "quantity" => filter.IsDescending
                ? query.OrderByDescending(s => s.Quantity)
                : query.OrderBy(s => s.Quantity),

            "purchaseprice" => filter.IsDescending
                ? query.OrderByDescending(s => s.PurchasePrice)
                : query.OrderBy(s => s.PurchasePrice),

            _ => filter.IsDescending
                ? query.OrderByDescending(s => s.Id)
                : query.OrderBy(s => s.Id),
        };

        return await query
            .ProjectTo<SupplySetItem>(mapper.ConfigurationProvider, ct)
            .Skip((filter.Page - 1) * filter.Limit)
            .Take(filter.Limit)
            .ToListAsync(ct);
    }
    public async Task<int> GetCount(SupplySetFilter filter, CancellationToken ct)
    {
        var query = context.SupplySets.AsNoTracking();

        query = ApplyFilter(query, filter);

        return await query.CountAsync(ct);
    }

    public async Task<long> Create(SupplySet supplySet, CancellationToken ct)
    {
        var supplySetEntity = new SupplySetEntity
        {
            SupplyId = supplySet.SupplyId,
            PositionId = supplySet.PositionId,
            Quantity = supplySet.Quantity,
            PurchasePrice = supplySet.PurchasePrice,
        };

        await context.SupplySets.AddAsync(supplySetEntity, ct);
        await context.SaveChangesAsync(ct);

        return supplySetEntity.Id;
    }

    public async Task<long> Update(long id, SupplySetUpdateModel model, CancellationToken ct)
    {
        var entity = await context.SupplySets.FirstOrDefaultAsync(s => s.Id == id, ct)
            ?? throw new NotFoundException("SupplySet note not found");

        if (model.Quantity.HasValue)
        {
            entity.Quantity = model.Quantity.Value;
        }

        if (model.PurchasePrice.HasValue)
        {
            entity.PurchasePrice = model.PurchasePrice.Value;
        }

        await context.SaveChangesAsync(ct);

        return entity.Id;
    }

    public async Task<long> Delete(long id, CancellationToken ct)
    {
        await context.SupplySets
            .Where(s => s.Id == id)
            .ExecuteDeleteAsync(ct);

        return id;
    }
}
