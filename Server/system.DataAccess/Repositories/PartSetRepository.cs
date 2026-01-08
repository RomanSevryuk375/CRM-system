using AutoMapper;
using AutoMapper.QueryableExtensions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels.PartSet;
using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;

namespace CRMSystem.DataAccess.Repositories;

public class PartSetRepository : IPartSetRepository
{
    private readonly SystemDbContext _context;
    private readonly IMapper _mapper;

    public PartSetRepository(
        SystemDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    private static IQueryable<PartSetEntity> ApplyFilter(IQueryable<PartSetEntity> query, PartSetFilter filter)
    {
        if (filter.OrderIds != null && filter.OrderIds.Any())
            query = query.Where(p => filter.OrderIds.Contains(p.OrderId));

        if (filter.PositionIds != null && filter.PositionIds.Any())
            query = query.Where(p => filter.PositionIds.Contains(p.PositionId));

        if (filter.ProposalIds != null && filter.ProposalIds.Any())
            query = query.Where(p => filter.ProposalIds.Contains(p.ProposalId));

        return query;
    }

    public async Task<List<PartSetItem>> GetPaged(PartSetFilter filter, CancellationToken ct)
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

        return await query
            .ProjectTo<PartSetItem>(_mapper.ConfigurationProvider, ct)
            .Skip((filter.Page - 1) * filter.Limit)
            .Take(filter.Limit)
            .ToListAsync(ct);
    }

    public async Task<List<PartSetItem>> GetByOrderId(long orderId, CancellationToken ct)
    {
        return await _context.PartSets
            .AsNoTracking()
            .Where(p => p.OrderId == orderId)
            .ProjectTo<PartSetItem>(_mapper.ConfigurationProvider, ct)
            .ToListAsync(ct);
    }

    public async Task<PartSetItem?> GetById(long id, CancellationToken ct)
    {
        return await _context.PartSets
            .AsNoTracking()
            .Where(p => p.Id == id)
            .ProjectTo<PartSetItem>(_mapper.ConfigurationProvider, ct)
            .FirstOrDefaultAsync(ct);
    }

    public async Task<int> GetCount(PartSetFilter filter, CancellationToken ct)
    {
        var query = _context.PartSets.AsNoTracking();
        query = ApplyFilter(query, filter);
        return await query.CountAsync(ct);
    }

    public async Task<long> Create(PartSet partSet, CancellationToken ct)
    {
        var partSetEntity = new PartSetEntity
        ( 
            partSet.OrderId,
            partSet.PositionId,
            partSet.ProposalId,
            partSet.Quantity,
            partSet.SoldPrice
        );

        await _context.PartSets.AddAsync(partSetEntity, ct);
        await _context.SaveChangesAsync(ct);

        return partSet.Id;
    }

    public async Task<long> Update(long id, PartSetUpdateModel model, CancellationToken ct)
    {
        var entity = await _context.PartSets.FirstOrDefaultAsync(p => p.Id == id, ct)
            ?? throw new Exception("Part not found");

        if (model.SoldPrice.HasValue) entity.SoldPrice = model.SoldPrice.Value;
        if (model.Quantity.HasValue) entity.Quantity = model.Quantity.Value;

        await _context.SaveChangesAsync(ct);

        return entity.Id;
    }

    public async Task<long> Delete(long id, CancellationToken ct)
    {
        var query = await _context.PartSets
            .Where(p => p.Id == id)
            .ExecuteDeleteAsync(ct);

        return id;
    }

    public async Task<long> DeleteProposedParts(long proposalId, CancellationToken ct)
    {
        var query = await _context.PartSets
            .Where(p => p.ProposalId == proposalId)
            .ExecuteDeleteAsync(ct);

        return proposalId;
    }

    public async Task<bool> Exists(long id, CancellationToken ct)
    {
        return await _context.PartSets
            .AsNoTracking()
            .AnyAsync(p => p.Id == id, ct);
    }

    public async Task<long> MoveFromProposalToOrder(long proposalId, long orderId, CancellationToken ct)
    {
        var parts = await _context.PartSets
            .Where(p => p.ProposalId == proposalId)
            .ToListAsync(ct);

        var transfer = parts.Select(p => new PartSetEntity(
            orderId,
            p.PositionId,
            null,
            p.Quantity,
            p.SoldPrice
        )).ToList();

        await _context.PartSets.AddRangeAsync(transfer, ct);
        await _context.SaveChangesAsync(ct);
        await _context.PartSets
            .Where(p => p.ProposalId == proposalId)
            .ExecuteDeleteAsync(ct);

        return orderId;
    }
}
