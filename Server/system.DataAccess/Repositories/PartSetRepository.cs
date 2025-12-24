using CRMSystem.Core.DTOs.PartSet;
using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;

namespace CRMSystem.DataAccess.Repositories;

public class PartSetRepository : IPartSetRepository
{
    private readonly SystemDbContext _context;

    public PartSetRepository(SystemDbContext context)
    {
        _context = context;
    }

    private IQueryable<PartSetEntity> ApplyFilter(IQueryable<PartSetEntity> query, PartSetFilter filter)
    {
        if (filter.orderIds != null && filter.orderIds.Any())
            query = query.Where(p => filter.orderIds.Contains(p.OrderId));

        if (filter.positionIds != null && filter.positionIds.Any())
            query = query.Where(p => filter.positionIds.Contains(p.PositionId));

        if (filter.proposalIds != null && filter.proposalIds.Any())
            query = query.Where(p => filter.proposalIds.Contains(p.ProposalId));

        return query;
    }

    public async Task<List<PartSetItem>> GetPaged(PartSetFilter filter)
    {
        var query = _context.PartSets.AsNoTracking();
        query = ApplyFilter(query, filter);

        query = filter.SortBy?.ToLower().Trim() switch
        {
            "order" => filter.isDescending
                ? query.OrderByDescending(p => p.OrderId)
                : query.OrderBy(p => p.OrderId),
            "position" => filter.isDescending
                ? query.OrderByDescending(p => p.Position == null
                    ? string.Empty
                    : p.Position.Part == null
                        ? string.Empty
                        : p.Position.Part.Name)
                : query.OrderBy(p => p.Position == null
                    ? string.Empty
                    : p.Position.Part == null
                        ? string.Empty
                        : p.Position.Part.Name),
            "proposal" => filter.isDescending
                ? query.OrderByDescending(p => p.ProposalId)
                : query.OrderBy(p => p.ProposalId),
            "quantity" => filter.isDescending
                ? query.OrderByDescending(p => p.Quantity)
                : query.OrderBy(p => p.Quantity),
            "soldprice" => filter.isDescending
                ? query.OrderByDescending(p => p.SoldPrice)
                : query.OrderBy(p => p.SoldPrice),

            _ => filter.isDescending
                ? query.OrderByDescending(p => p.Id)
                : query.OrderBy(p => p.Id),
        };

        var projection = query.Select(p => new PartSetItem(
            p.Id,
            p.OrderId,
            p.Position == null
                    ? string.Empty
                    : p.Position.Part == null
                        ? string.Empty
                        : p.Position.Part.Name,
            p.PositionId,
            p.ProposalId,
            p.Quantity,
            p.SoldPrice));

        return await projection
            .Skip((filter.Page - 1) * filter.Limit)
            .Take(filter.Limit)
            .ToListAsync();
    }

    public async Task<List<PartSetItem>> GetByOrderId(long orderId)
    {
        var partSets = await _context.PartSets
            .AsNoTracking()
            .Where(p => p.OrderId == orderId)
            .Select(p => new PartSetItem(
                p.Id,
                p.OrderId,
                p.Position == null
                        ? string.Empty
                        : p.Position.Part == null
                            ? string.Empty
                            : p.Position.Part.Name,
                p.PositionId,
                p.ProposalId,
                p.Quantity,
                p.SoldPrice))
            .ToListAsync();

        return partSets;
    }
 
    public async Task<long> Create(PartSet partSet)
    {
        var partSetEntity = new PartSetEntity
        {
            OrderId = partSet.OrderId,
            PositionId = partSet.PositionId,
            ProposalId = partSet.ProposalId,
            Quantity = partSet.Quantity,
            SoldPrice = partSet.SoldPrice,
        };

        await _context.PartSets.AddAsync(partSetEntity);
        await _context.SaveChangesAsync();

        return partSet.Id;
    }

    public async Task<long> Update(long id, PartSetUpdateModel model)
    {
        var entity = await _context.PartSets.FirstOrDefaultAsync(p => p.Id == id)
            ?? throw new Exception("Part not found");

        if (model.soldPrice.HasValue) entity.SoldPrice = model.soldPrice.Value;
        if (model.quantity.HasValue) entity.Quantity = model.quantity.Value;

        await _context.SaveChangesAsync();

        return entity.Id;
    }

    public async Task<long> Delete(long id)
    {
        var query = await _context.PartSets
            .Where(p => p.Id == id)
            .ExecuteDeleteAsync();

        return id;
    }

    public async Task<bool> Exists(long id)
    {
        return await _context.PartSets
            .AsNoTracking()
            .AnyAsync(p => p.Id == id);
    }
}
