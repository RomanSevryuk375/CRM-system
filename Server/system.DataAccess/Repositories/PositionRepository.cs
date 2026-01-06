using AutoMapper;
using AutoMapper.QueryableExtensions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels.Position;
using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;

namespace CRMSystem.DataAccess.Repositories;

public class PositionRepository : IPositionRepository
{
    private readonly SystemDbContext _context;
    private readonly IMapper _mapper;

    public PositionRepository(
        SystemDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    private IQueryable<PositionEntity> ApplyFilter(IQueryable<PositionEntity> query, PositionFilter filter)
    {
        if (filter.PartIds != null && filter.PartIds.Any())
            query = query.Where(p => filter.PartIds.Contains(p.PartId));

        return query;
    }

    public async Task<List<PositionItem>> GetPaged(PositionFilter filter)
    {
        var query = _context.Positions.AsNoTracking();
        query = ApplyFilter(query, filter);

        query = filter.SortBy?.ToLower().Trim() switch
        {
            "part" => filter.IsDescending
                ? query.OrderByDescending(p => p.Part == null
                    ? string.Empty
                    : p.Part.Name)
                : query.OrderBy(p => p.Part == null
                    ? string.Empty
                    : p.Part.Name),
            "purchaseprice" => filter.IsDescending
                ? query.OrderByDescending(p => p.PurchasePrice)
                : query.OrderBy(p => p.PurchasePrice),
            "sellingprice" => filter.IsDescending
                ? query.OrderByDescending(p => p.SellingPrice)
                : query.OrderBy(p => p.SellingPrice),
            "quantity" => filter.IsDescending
                ? query.OrderByDescending(p => p.Quantity)
                : query.OrderBy(p => p.Quantity),
            "cell" => filter.IsDescending
                ? query.OrderByDescending(p => p.CellId)
                : query.OrderBy(p => p.CellId),

            _ => filter.IsDescending
                ? query.OrderByDescending(p => p.Id)
                : query.OrderBy(p => p.Id),
        };

        return await query
            .ProjectTo<PositionItem>(_mapper.ConfigurationProvider)
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

        if (model.CellId.HasValue) entity.CellId = model.CellId.Value;
        if (model.PurchasePrice.HasValue) entity.PurchasePrice = model.PurchasePrice.Value;
        if (model.SellingPrice.HasValue) entity.SellingPrice = model.SellingPrice.Value;
        if (model.Quantity.HasValue) entity.Quantity = model.Quantity.Value;

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
