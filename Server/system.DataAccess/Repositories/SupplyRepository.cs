using CRMSystem.Core.DTOs.Supply;
using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;

namespace CRMSystem.DataAccess.Repositories;

public class SupplyRepository : ISypplyRepository
{
    private readonly SystemDbContext _context;

    public SupplyRepository(SystemDbContext context)
    {
        _context = context;
    }

    private IQueryable<SupplyEntity> ApplyFilter(IQueryable<SupplyEntity> query, SupplyFilter filter)
    {
        if (filter.suplierIds != null && filter.suplierIds.Any())
            query = query.Where(s => filter.suplierIds.Contains(s.SupplierId));

        return query;
    }

    public async Task<List<SupplyItem>> GetPaged(SupplyFilter filter)
    {
        var query = _context.Supplies.AsNoTracking();
        query = ApplyFilter(query, filter);

        query = filter.SortBy?.ToLower().Trim() switch
        {
            "supplier" => filter.isDescending
                ? query.OrderByDescending(s => s.Supplier == null
                    ? string.Empty
                    : s.Supplier.Name)
                : query.OrderBy(s => s.Supplier == null
                    ? string.Empty
                    : s.Supplier.Name),
            "date" => filter.isDescending
                ? query.OrderByDescending(s => s.Date)
                : query.OrderBy(s => s.Date),

            _ => filter.isDescending
                ? query.OrderByDescending(s => s.Id)
                : query.OrderBy(s => s.Id),
        };

        var projection = query.Select(s => new SupplyItem(
            s.Id,
            s.Supplier == null
                ? string.Empty
                : s.Supplier.Name,
            s.Date));

        return await projection
            .Skip((filter.Page - 1) * filter.Limit)
            .Take(filter.Limit)
            .ToListAsync();
    }

    public async Task<long> Create(Supply supply)
    {
        var sullpyEntity = new SupplyEntity
        {
            SupplierId = supply.SupplierId,
            Date = supply.Date,
        };

        await _context.Supplies.AddAsync(sullpyEntity);
        await _context.SaveChangesAsync();

        return sullpyEntity.Id;
    }

    public async Task<long> Delete(long id)
    {
        var enity = await _context.Supplies
            .Where(s => s.Id == id)
            .ExecuteDeleteAsync();

        return id;
    }
}
