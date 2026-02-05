using AutoMapper;
using AutoMapper.QueryableExtensions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels.PartCategory;
using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;
using CRMSystem.Core.Exceptions;

namespace CRMSystem.DataAccess.Repositories;

public class PartCategoryRepository(
    SystemDbContext context,
    IMapper mapper) : IPartCategoryRepository
{
    public async Task<List<PartCategoryItem>> Get(CancellationToken ct)
    {
        return await context.PartCategories
            .AsNoTracking()
            .ProjectTo<PartCategoryItem>(mapper.ConfigurationProvider, ct)
            .ToListAsync(ct);
    }

    public async Task<int> Create(PartCategory category, CancellationToken ct)
    {
        var partCategoryEntity = new PartCategoryEntity
        {
            Name = category.Name,
            Description = category.Description,
        };

        await context.AddAsync(partCategoryEntity, ct);
        await context.SaveChangesAsync(ct);

        return partCategoryEntity.Id;
    }

    public async Task<int> Update(int id, PartCategoryUpdateModel model, CancellationToken ct)
    {
        var entity = await context.PartCategories.FirstOrDefaultAsync(p => p.Id == id, ct)
            ?? throw new NotFoundException("PartCategory not found");

        if (!string.IsNullOrWhiteSpace(model.Name))
        {
            entity.Name = model.Name;
        }

        if (!string.IsNullOrWhiteSpace(model.Description))
        {
            entity.Description = model.Description;
        }

        await context.SaveChangesAsync(ct);

        return entity.Id;
    }

    public async Task<int> Delete(int id, CancellationToken ct)
    {
        await context.PartCategories
            .Where(p => p.Id == id)
            .ExecuteDeleteAsync(ct);

        return id;
    }

    public async Task<bool> Exists (int id, CancellationToken ct)
    {
        return await context.PartCategories
            .AsNoTracking()
            .AnyAsync(p => p.Id == id, ct);
    }

    public async Task<bool> NameExists (string name, CancellationToken ct)
    {
        return await context.PartCategories
            .AsNoTracking()
            .AnyAsync(p => p.Name == name, ct);
    }
}
