using CRMSystem.Core.DTOs.Order;
using CRMSystem.Core.DTOs.Part;
using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;

namespace CRMSystem.DataAccess.Repositories;

public class PartRepositiry : IPartRepositiry
{
    private readonly SystemDbContext _context;

    public PartRepositiry(SystemDbContext context)
    {
        _context = context;
    }

    private IQueryable<PartEntity> ApplyFilter(IQueryable<PartEntity> query, PartFilter filter)
    {
        if (filter.categoryIds != null && filter.categoryIds.Any())
            query = query.Where(p => filter.categoryIds.Contains(p.CategoryId));

        return query;
    }

    public async Task<List<PartItem>> GetPaged(PartFilter filter)
    {
        var query = _context.Parts.AsNoTracking();
        query = ApplyFilter(query, filter);

        query = filter.SortBy?.ToLower().Trim() switch
        {
            "categoryid" => filter.isDescending
                ? query.OrderByDescending(p => p.PartCategory == null
                    ? string.Empty
                    : p.PartCategory.Name)
                : query.OrderBy(p => p.PartCategory == null
                    ? string.Empty
                    : p.PartCategory.Name),
            "oemarticle" => filter.isDescending
                ? query.OrderByDescending(p => p.OEMArticle)
                : query.OrderBy(p => p.OEMArticle),
            "manufacturerarticle" => filter.isDescending
                ? query.OrderByDescending(p => p.ManufacturerArticle)
                : query.OrderBy(p => p.ManufacturerArticle),
            "manufacturer" => filter.isDescending
                ? query.OrderByDescending(p => p.Manufacturer)
                : query.OrderBy(p => p.Manufacturer),
            "internalarticle" => filter.isDescending
                ? query.OrderByDescending(p => p.InternalArticle)
                : query.OrderBy(p => p.InternalArticle),
            "description" => filter.isDescending
                ? query.OrderByDescending(p => p.Description)
                : query.OrderBy(p => p.Description),
            "name" => filter.isDescending
                ? query.OrderByDescending(p => p.Name)
                : query.OrderBy(p => p.Name),

            _ => filter.isDescending
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

    public async Task<int> GetCoumt(PartFilter filter)
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

        if (!string.IsNullOrWhiteSpace(model.oemArticle)) entity.OEMArticle = model.oemArticle;
        if (!string.IsNullOrWhiteSpace(model.manufacturerArticle)) entity.ManufacturerArticle = model.manufacturerArticle;
        if (!string.IsNullOrWhiteSpace(model.internalArticle)) entity.InternalArticle = model.internalArticle;
        if (!string.IsNullOrWhiteSpace(model.description)) entity.Description = model.description;
        if (!string.IsNullOrWhiteSpace(model.name)) entity.Name = model.name;
        if (!string.IsNullOrWhiteSpace(model.manufacturer)) entity.Manufacturer = model.manufacturer;
        if (!string.IsNullOrWhiteSpace(model.applicability)) entity.Applicability = model.applicability;

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
}
