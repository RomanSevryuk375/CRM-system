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
        if (filter.OrderIds != null && filter.OrderIds.Any())
            query = query.Where(p => filter.OrderIds.Contains(p.OrderId));

        if (filter.PositionIds != null && filter.PositionIds.Any())
            query = query.Where(p => filter.PositionIds.Contains(p.PositionId));

        if (filter.ProposalIds != null && filter.ProposalIds.Any())
            query = query.Where(p => filter.ProposalIds.Contains(p.ProposalId));

        return query;
    }

    public async Task<List<PartSetItem>> GetPaged(PartSetFilter filter)
    {
        var query = _context.PartSets.AsNoTracking();
        query = ApplyFilter(query, filter);

        query = filter.SortBy?.ToLower().Trim() switch
        {
            "order" => filter.IsDescending
                ? query.OrderByDescending(p => p.OrderId)
                : query.OrderBy(p => p.OrderId),
            "position" => filter.IsDescending
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
            "proposal" => filter.IsDescending
                ? query.OrderByDescending(p => p.ProposalId)
                : query.OrderBy(p => p.ProposalId),
            "quantity" => filter.IsDescending
                ? query.OrderByDescending(p => p.Quantity)
                : query.OrderBy(p => p.Quantity),
            "soldprice" => filter.IsDescending
                ? query.OrderByDescending(p => p.SoldPrice)
                : query.OrderBy(p => p.SoldPrice),

            _ => filter.IsDescending
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
        return await _context.PartSets
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
    }

    public async Task<PartSetItem?> GetById(long id)
    {
        return await _context.PartSets
            .AsNoTracking()
            .Where(p => p.Id == id)
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
            .FirstOrDefaultAsync();
    }

    public async Task<int> GetCount(PartSetFilter filter)
    {
        var query = _context.PartSets.AsNoTracking();
        query = ApplyFilter(query, filter);
        return await query.CountAsync();
    }

    public async Task<long> Create(PartSet partSet)
    {
        var partSetEntity = new PartSetEntity
        ( 
            partSet.OrderId,
            partSet.PositionId,
            partSet.ProposalId,
            partSet.Quantity,
            partSet.SoldPrice
        );

        await _context.PartSets.AddAsync(partSetEntity);
        await _context.SaveChangesAsync();

        return partSet.Id;
    }

    public async Task<long> Update(long id, PartSetUpdateModel model)
    {
        var entity = await _context.PartSets.FirstOrDefaultAsync(p => p.Id == id)
            ?? throw new Exception("Part not found");

        if (model.SoldPrice.HasValue) entity.SoldPrice = model.SoldPrice.Value;
        if (model.Quantity.HasValue) entity.Quantity = model.Quantity.Value;

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

    public async Task<long> DeleteProposedParts(long proposalId)
    {
        var query = await _context.PartSets
            .Where(p => p.ProposalId == proposalId)
            .ExecuteDeleteAsync();

        return proposalId;
    }

    public async Task<bool> Exists(long id)
    {
        return await _context.PartSets
            .AsNoTracking()
            .AnyAsync(p => p.Id == id);
    }

    public async Task<long> MoveFromProposalToOrder(long proposalId, long orderId)
    {
        var parts = await _context.PartSets
            .Where(p => p.ProposalId == proposalId)
            .ToListAsync();

        var transfer = parts.Select(p => new PartSetEntity(
            orderId,
            p.PositionId,
            null,
            p.Quantity,
            p.SoldPrice
        )).ToList();

        await _context.PartSets.AddRangeAsync(transfer);
        await _context.SaveChangesAsync();
        await _context.PartSets
            .Where(p => p.ProposalId == proposalId)
            .ExecuteDeleteAsync();

        return orderId;
    }
}
