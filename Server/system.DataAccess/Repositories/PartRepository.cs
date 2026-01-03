using CRMSystem.Core.DTOs.Order;
using CRMSystem.Core.DTOs.Part;
using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;

namespace CRMSystem.DataAccess.Repositories;

public class PartRepository : IPartRepository
{
    private readonly SystemDbContext _context;

    public PartRepository(SystemDbContext context)
    {
        _context = context;
    }

    private IQueryable<PartEntity> ApplyFilter(IQueryable<PartEntity> query, PartFilter filter)
    {
        if (filter.CategoryIds != null && filter.CategoryIds.Any())
            query = query.Where(p => filter.CategoryIds.Contains(p.CategoryId));

        return query;
    }

    public async Task<List<PartItem>> GetPaged(PartFilter filter)
    {
        var query = _context.Parts.AsNoTracking();
        query = ApplyFilter(query, filter);

        query = filter.SortBy?.ToLower().Trim() switch
        {
            "categoryid" => filter.IsDescending
                ? query.OrderByDescending(p => p.PartCategory == null
                    ? string.Empty
                    : p.PartCategory.Name)
                : query.OrderBy(p => p.PartCategory == null
                    ? string.Empty
                    : p.PartCategory.Name),
            "oemarticle" => filter.IsDescending
                ? query.OrderByDescending(p => p.OEMArticle)
                : query.OrderBy(p => p.OEMArticle),
            "manufacturerarticle" => filter.IsDescending
                ? query.OrderByDescending(p => p.ManufacturerArticle)
                : query.OrderBy(p => p.ManufacturerArticle),
            "manufacturer" => filter.IsDescending
                ? query.OrderByDescending(p => p.Manufacturer)
                : query.OrderBy(p => p.Manufacturer),
            "internalarticle" => filter.IsDescending
                ? query.OrderByDescending(p => p.InternalArticle)
                : query.OrderBy(p => p.InternalArticle),
            "description" => filter.IsDescending
                ? query.OrderByDescending(p => p.Description)
                : query.OrderBy(p => p.Description),
            "name" => filter.IsDescending
                ? query.OrderByDescending(p => p.Name)
                : query.OrderBy(p => p.Name),

            _ => filter.IsDescending
                ? query.OrderByDescending(p => p.Id)
                : query.OrderBy(p => p.Id),
        };

        var projection = query.Select(p => new PartItem(
            p.Id,
            p.PartCategory == null
                ? string.Empty
                : p.PartCategory.Name,
            p.CategoryId,
            p.OEMArticle,
            p.ManufacturerArticle,
            p.InternalArticle,
            p.Description,
            p.Name,
            p.Manufacturer,
            p.Applicability));

        return await projection
            .Skip((filter.Page - 1) * filter.Limit)
            .Take(filter.Limit)
            .ToListAsync();
    }

    public async Task<int> GetCount(PartFilter filter)
    {
        var query = _context.Parts.AsNoTracking();
        query = ApplyFilter(query, filter);
        return await query.CountAsync();
    }

    public async Task<long> Create(Part part)
    {
        var partEntity = new PartEntity
        {
            CategoryId = part.CategoryId,
            OEMArticle = part.OEMArticle,
            ManufacturerArticle = part.ManufacturerArticle,
            InternalArticle = part.InternalArticle,
            Description = part.Description,
            Name = part.Name,
            Manufacturer = part.Manufacturer,
            Applicability = part.Applicability
        };

        await _context.Parts.AddAsync(partEntity);
        await _context.SaveChangesAsync();

        return partEntity.Id;
    }

    public async Task<long> Update(long id, PartUpdateModel model)
    {
        var entity = await _context.Parts.FirstOrDefaultAsync(p => p.Id == id)
            ?? throw new Exception("Part not found");

        if (!string.IsNullOrWhiteSpace(model.OemArticle)) entity.OEMArticle = model.OemArticle;
        if (!string.IsNullOrWhiteSpace(model.ManufacturerArticle)) entity.ManufacturerArticle = model.ManufacturerArticle;
        if (!string.IsNullOrWhiteSpace(model.InternalArticle)) entity.InternalArticle = model.InternalArticle;
        if (!string.IsNullOrWhiteSpace(model.Description)) entity.Description = model.Description;
        if (!string.IsNullOrWhiteSpace(model.Name)) entity.Name = model.Name;
        if (!string.IsNullOrWhiteSpace(model.Manufacturer)) entity.Manufacturer = model.Manufacturer;
        if (!string.IsNullOrWhiteSpace(model.Applicability)) entity.Applicability = model.Applicability;

        await _context.SaveChangesAsync();

        return entity.Id;
    }

    public async Task<long> Delete(long id)
    {
        var entity = await _context.Parts
            .Where(p => p.Id == id)
            .ExecuteDeleteAsync();

        return id;
    }

    public async Task<bool> Exists(long id)
    {
        return await _context.Parts
            .AsNoTracking()
            .AnyAsync(p => p.Id == id);
    }
}
