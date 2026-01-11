using AutoMapper;
using AutoMapper.QueryableExtensions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels.Part;
using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;
using CRMSystem.Core.Exceptions;

namespace CRMSystem.DataAccess.Repositories;

public class PartRepository : IPartRepository
{
    private readonly SystemDbContext _context;
    private readonly IMapper _mapper;

    public PartRepository(
        SystemDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    private static IQueryable<PartEntity> ApplyFilter(IQueryable<PartEntity> query, PartFilter filter)
    {
        if (filter.CategoryIds != null && filter.CategoryIds.Any())
            query = query.Where(p => filter.CategoryIds.Contains(p.CategoryId));

        return query;
    }

    public async Task<List<PartItem>> GetPaged(PartFilter filter, CancellationToken ct)
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

        return await query
            .ProjectTo<PartItem>(_mapper.ConfigurationProvider, ct)
            .Skip((filter.Page - 1) * filter.Limit)
            .Take(filter.Limit)
            .ToListAsync(ct);
    }

    public async Task<int> GetCount(PartFilter filter, CancellationToken ct)
    {
        var query = _context.Parts.AsNoTracking();
        query = ApplyFilter(query, filter);
        return await query.CountAsync(ct);
    }

    public async Task<long> Create(Part part, CancellationToken ct)
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

        await _context.Parts.AddAsync(partEntity, ct);
        await _context.SaveChangesAsync(ct);

        return partEntity.Id;
    }

    public async Task<long> Update(long id, PartUpdateModel model, CancellationToken ct)
    {
        var entity = await _context.Parts.FirstOrDefaultAsync(p => p.Id == id, ct)
            ?? throw new NotFoundException("Part not found");

        if (!string.IsNullOrWhiteSpace(model.OemArticle)) entity.OEMArticle = model.OemArticle;
        if (!string.IsNullOrWhiteSpace(model.ManufacturerArticle)) entity.ManufacturerArticle = model.ManufacturerArticle;
        if (!string.IsNullOrWhiteSpace(model.InternalArticle)) entity.InternalArticle = model.InternalArticle;
        if (!string.IsNullOrWhiteSpace(model.Description)) entity.Description = model.Description;
        if (!string.IsNullOrWhiteSpace(model.Name)) entity.Name = model.Name;
        if (!string.IsNullOrWhiteSpace(model.Manufacturer)) entity.Manufacturer = model.Manufacturer;
        if (!string.IsNullOrWhiteSpace(model.Applicability)) entity.Applicability = model.Applicability;

        await _context.SaveChangesAsync(ct);

        return entity.Id;
    }

    public async Task<long> Delete(long id, CancellationToken ct)
    {
        var entity = await _context.Parts
            .Where(p => p.Id == id)
            .ExecuteDeleteAsync(ct);

        return id;
    }

    public async Task<bool> Exists(long id, CancellationToken ct)
    {
        return await _context.Parts
            .AsNoTracking()
            .AnyAsync(p => p.Id == id, ct);
    }
}
