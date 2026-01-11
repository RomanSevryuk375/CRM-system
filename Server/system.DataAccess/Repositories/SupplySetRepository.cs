using AutoMapper;
using AutoMapper.QueryableExtensions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels.SupplySet;
using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;
using CRMSystem.Core.Exceptions;

namespace CRMSystem.DataAccess.Repositories;

public class SupplySetRepository : ISupplySetRepository
{
    private readonly SystemDbContext _context;
    private readonly IMapper _mapper;

    public SupplySetRepository(
        SystemDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    private static IQueryable<SupplySetEntity> ApplyFilter(IQueryable<SupplySetEntity> query, SupplySetFilter filter)
    {
        if (filter.SupplyIds != null && filter.SupplyIds.Any())
            query = query.Where(s => filter.SupplyIds.Contains(s.SupplyId));

        if (filter.PositionIds != null && filter.PositionIds.Any())
            query = query.Where(s => filter.PositionIds.Contains(s.PositionId));

        return query;
    }

    public async Task<List<SupplySetItem>> GetPaged(SupplySetFilter filter, CancellationToken ct)
    {
        var query = _context.SupplySets.AsNoTracking();
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
            .ProjectTo<SupplySetItem>(_mapper.ConfigurationProvider, ct)
            .Skip((filter.Page - 1) * filter.Limit)
            .Take(filter.Limit)
            .ToListAsync(ct);
    }
    public async Task<int> GetCount(SupplySetFilter filter, CancellationToken ct)
    {
        var query = _context.SupplySets.AsNoTracking();
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

        await _context.SupplySets.AddAsync(supplySetEntity, ct);
        await _context.SaveChangesAsync(ct);

        return supplySetEntity.Id;
    }

    public async Task<long> Update(long id, SupplySetUpdateModel model, CancellationToken ct)
    {
        var entity = await _context.SupplySets.FirstOrDefaultAsync(s => s.Id == id, ct)
            ?? throw new NotFoundException("SupplySet note not found");

        if (model.Quantity.HasValue) entity.Quantity = model.Quantity.Value;
        if (model.PurchasePrice.HasValue) entity.PurchasePrice = model.PurchasePrice.Value;

        await _context.SaveChangesAsync(ct);

        return entity.Id;
    }

    public async Task<long> Delete(long id, CancellationToken ct)
    {
        var entity = await _context.SupplySets
            .Where(s => s.Id == id)
            .ExecuteDeleteAsync(ct);

        return id;
    }
}
