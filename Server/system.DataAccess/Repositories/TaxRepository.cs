using CRMSystem.Core.DTOs.Tax;
using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;

namespace CRMSystem.DataAccess.Repositories;

public class TaxRepository : ITaxRepository
{
    private readonly SystemDbContext _context;

    public TaxRepository(SystemDbContext context)
    {
        _context = context;
    }

    private IQueryable<TaxEntity> ApplyFilter(IQueryable<TaxEntity> query, TaxFilter filter)
    {
        if (filter.TaxTyprIds != null && filter.TaxTyprIds.Any())
            query = query.Where(t => filter.TaxTyprIds.Contains(t.TypeId));

        return query;
    }

    public async Task<List<TaxItem>> Get(TaxFilter filter)
    {
        var query = _context.Taxes.AsNoTracking();
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

        var projection = query.Select(
            t => new TaxItem(
                t.Id,
                t.Name,
                t.Rate,
                t.TaxType == null
                    ? string.Empty
                    : t.TaxType.Name));

        return await projection.ToListAsync();
    }

    public async Task<int> Create(Tax tax)
    {

        var taxEntitie = new TaxEntity
        {
            Name = tax.Name,
            Rate = tax.Rate,
            TypeId = (int)tax.TypeId,
        };

        await _context.Taxes.AddAsync(taxEntitie);
        await _context.SaveChangesAsync();

        return taxEntitie.Id;
    }

    public async Task<int> Update(int id, TaxUpdateModel model)
    {
        var entity = _context.Taxes.FirstOrDefault(t => t.Id == id)
            ?? throw new ArgumentException("Tax not found");

        if (!string.IsNullOrWhiteSpace(model.Name)) entity.Name = model.Name;
        if (model.Rate.HasValue) entity.Rate = model.Rate.Value;

        await _context.SaveChangesAsync();

        return entity.Id;
    }

    public async Task<int> Delete(int id)
    {
        var entity = await _context.Taxes
            .Where(t => t.Id == id)
            .ExecuteDeleteAsync();

        return id;
    }

    public async Task<bool> Exists (int id)
    {
        return await _context.Taxes
            .AsNoTracking()
            .AnyAsync(t => t.Id == id);
    }
}
