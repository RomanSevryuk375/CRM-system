using CRMSystem.Core.DTOs.PartCategory;
using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;

namespace CRMSystem.DataAccess.Repositories;

public class PartCategoryRepository : IPartCategoryRepository
{
    private readonly SystemDbContext _context;

    public PartCategoryRepository(SystemDbContext context)
    {
        _context = context;
    }

    public async Task<List<PartCategoryItem>> Get()
    {
        var query = _context.PartCategories.AsNoTracking();

        var projection = query.Select(p => new PartCategoryItem(
            p.Id,
            p.Name,
            p.Description));

        return await projection.ToListAsync();
    }

    public async Task<int> Create(PartCategory category)
    {
        var partCategoryEntity = new PartCategoryEntity
        {
            Name = category.Name,
            Description = category.Description,
        };

        await _context.AddAsync(partCategoryEntity);
        await _context.SaveChangesAsync();

        return partCategoryEntity.Id;
    }

    public async Task<int> Update(int id, PartCategoryUpdateModel model)
    {
        var entity = await _context.PartCategories.FirstOrDefaultAsync(p => p.Id == id)
            ?? throw new Exception("PartCategory not found");

        if (!string.IsNullOrWhiteSpace(model.name)) entity.Name = model.name;
        if (!string.IsNullOrWhiteSpace(model.description)) entity.Description = model.description;

        await _context.SaveChangesAsync();

        return entity.Id;
    }

    public async Task<int> Delete(int id)
    {
        var entity = await _context.PartCategories
            .Where(p => p.Id == id)
            .ExecuteDeleteAsync();

        return id;
    }
}
