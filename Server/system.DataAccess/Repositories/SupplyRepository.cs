using AutoMapper;
using AutoMapper.QueryableExtensions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels.Supply;
using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;
using Shared.Filters;

namespace CRMSystem.DataAccess.Repositories;

public class SupplyRepository(
    SystemDbContext context,
    IMapper mapper) : ISupplyRepository
{
    private static IQueryable<SupplyEntity> ApplyFilter(IQueryable<SupplyEntity> query, SupplyFilter filter)
    {
        if (filter.SuplierIds != null && filter.SuplierIds.Any())
        {
            query = query.Where(s => filter.SuplierIds.Contains(s.SupplierId));
        }

        return query;
    }

    public async Task<List<SupplyItem>> GetPaged(SupplyFilter filter, CancellationToken ct)
    {
        var query = context.Supplies.AsNoTracking();
        query = ApplyFilter(query, filter);

        query = filter.SortBy?.ToLower().Trim() switch
        {
            "supplier" => filter.IsDescending
                ? query.OrderByDescending(s => s.Supplier == null
                    ? string.Empty
                    : s.Supplier.Name)
                : query.OrderBy(s => s.Supplier == null
                    ? string.Empty
                    : s.Supplier.Name),

            "date" => filter.IsDescending
                ? query.OrderByDescending(s => s.Date)
                : query.OrderBy(s => s.Date),

            _ => filter.IsDescending
                ? query.OrderByDescending(s => s.Id)
                : query.OrderBy(s => s.Id),
        };

        return await query
            .ProjectTo<SupplyItem>(mapper.ConfigurationProvider, ct)
            .Skip((filter.Page - 1) * filter.Limit)
            .Take(filter.Limit)
            .ToListAsync(ct);
    }

    public async Task<int> GetCount (SupplyFilter filter, CancellationToken ct)
    {
        var query = context.Supplies.AsNoTracking();

        query = ApplyFilter(query, filter);

        return await query.CountAsync(ct);
    }

    public async Task<long> Create(Supply supply, CancellationToken ct)
    {
        var sullpyEntity = new SupplyEntity
        {
            SupplierId = supply.SupplierId,
            Date = supply.Date,
        };

        await context.Supplies.AddAsync(sullpyEntity, ct);
        await context.SaveChangesAsync(ct);

        return sullpyEntity.Id;
    }

    public async Task<long> Delete(long id, CancellationToken ct)
    {
        await context.Supplies
            .Where(s => s.Id == id)
            .ExecuteDeleteAsync(ct);

        return id;
    }

    public async Task<bool> Exists(long id, CancellationToken ct)
    {
        return await context.Supplies
            .AsNoTracking()
            .AnyAsync(s => s.Id == id, ct);
    }
}
