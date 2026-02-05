using AutoMapper;
using AutoMapper.QueryableExtensions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels.Tax;
using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;
using CRMSystem.Core.Exceptions;
using Shared.Filters;

namespace CRMSystem.DataAccess.Repositories;

public class TaxRepository(
    SystemDbContext context,
    IMapper mapper) : ITaxRepository
{
    private static IQueryable<TaxEntity> ApplyFilter(IQueryable<TaxEntity> query, TaxFilter filter)
    {
        if (filter.TaxTyprIds != null && filter.TaxTyprIds.Any())
        {
            query = query.Where(t => filter.TaxTyprIds.Contains(t.TypeId));
        }

        return query;
    }

    public async Task<List<TaxItem>> Get(TaxFilter filter, CancellationToken ct)
    {
        var query = context.Taxes.AsNoTracking();
        query = ApplyFilter(query, filter);

        query = filter.SortBy?.ToLower().Trim() switch
        {
            "name" => filter.IsDescending
                ? query.OrderByDescending(t => t.Name)
                : query.OrderBy(t => t.Name),

            "rate" => filter.IsDescending
                ? query.OrderByDescending(t => t.Rate)
                : query.OrderBy(t => t.Rate),

            "type" => filter.IsDescending
                ? query.OrderByDescending(t => t.TaxType == null
                    ? string.Empty
                    : t.TaxType.Name)
                : query.OrderBy(t => t.TaxType == null
                    ? string.Empty
                    : t.TaxType.Name),

            _ => filter.IsDescending
                ? query.OrderByDescending(t => t.Id)
                : query.OrderBy(t => t.Id),
        };

        return await query
            .ProjectTo<TaxItem>(mapper.ConfigurationProvider, ct)
            .ToListAsync(ct);
    }

    public async Task<int> Create(Tax tax, CancellationToken ct)
    {

        var taxEntitie = new TaxEntity
        {
            Name = tax.Name,
            Rate = tax.Rate,
            TypeId = (int)tax.TypeId,
        };

        await context.Taxes.AddAsync(taxEntitie, ct);
        await context.SaveChangesAsync(ct);

        return taxEntitie.Id;
    }

    public async Task<int> Update(int id, TaxUpdateModel model, CancellationToken ct)
    {
        var entity = await context.Taxes.FirstOrDefaultAsync(t => t.Id == id, ct)
            ?? throw new NotFoundException("Tax not found");

        if (!string.IsNullOrWhiteSpace(model.Name))
        {
            entity.Name = model.Name;
        }

        if (model.Rate.HasValue)
        {
            entity.Rate = model.Rate.Value;
        }

        await context.SaveChangesAsync(ct);

        return entity.Id;
    }

    public async Task<int> Delete(int id, CancellationToken ct)
    {
        await context.Taxes
            .Where(t => t.Id == id)
            .ExecuteDeleteAsync(ct);

        return id;
    }

    public async Task<bool> Exists (int id, CancellationToken ct)
    {
        return await context.Taxes
            .AsNoTracking()
            .AnyAsync(t => t.Id == id, ct);
    }
}
