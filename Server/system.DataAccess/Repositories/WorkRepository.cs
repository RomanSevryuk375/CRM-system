using CRMSystem.Core.DTOs.Work;
using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;

namespace CRMSystem.DataAccess.Repositories;

public class WorkRepository : IWorkRepository
{
    private readonly SystemDbContext _context;

    public WorkRepository(SystemDbContext context)
    {
        _context = context;
    }

    public async Task<List<WorkItem>> GetPaged(WorkFilter filter)
    {
        var query = _context.Works.AsNoTracking();

        query = filter.SortBy?.ToLower().Trim() switch
        {
            "title" => filter.isDescending
                ? query.OrderByDescending(w => w.Title)
                : query.OrderBy(w => w.Title),
            "category" => filter.isDescending
                ? query.OrderByDescending(w => w.Category)
                : query.OrderBy(w => w.Category),
            "description" => filter.isDescending
                ? query.OrderByDescending(w => w.Description)
                : query.OrderBy(w => w.Description),
            "standarttime" => filter.isDescending
                ? query.OrderByDescending(w => w.StandardTime)
                : query.OrderBy(w => w.StandardTime),


            _ => filter.isDescending
                ? query.OrderByDescending(w => w.Id)
                : query.OrderBy(w => w.Id),
        };

        var projection = query.Select(w => new WorkItem(
            w.Id,
            w.Title,
            w.Category,
            w.Description,
            w.StandardTime));

        return await projection
            .Skip((filter.Page - 1) * filter.Limit)
            .Take(filter.Limit)
            .ToListAsync();
    }

    public async Task<WorkItem?> GetById (long id)
    {
        return await _context.Works
            .AsNoTracking()
            .Where(w => w.Id == id)
            .Select(w => new WorkItem(
            w.Id,
            w.Title,
            w.Category,
            w.Description,
            w.StandardTime))
            .FirstOrDefaultAsync();

    }

    public async Task<int> GetCount()
    {
        return await _context.Works.CountAsync();
    }

    public async Task<long> Create(Work work)
    {
        var workEntity = new WorkEntity
        {
            Title = work.Title,
            Category = work.Category,
            Description = work.Description,
            StandardTime = work.StandardTime,
        };

        await _context.Works.AddAsync(workEntity);
        await _context.SaveChangesAsync();

        return workEntity.Id;
    }

    public async Task<long> Update(long id, WorkUpdateModel model)
    {
        var entty = await _context.Works.FirstOrDefaultAsync(w => w.Id == id)
            ?? throw new ArgumentException("Work not found");

        if (!string.IsNullOrWhiteSpace(model.title)) entty.Title = model.title;
        if (!string.IsNullOrWhiteSpace(model.categoty)) entty.Category = model.categoty;
        if (!string.IsNullOrWhiteSpace(model.description)) entty.Description = model.description;
        if (model.standartTime.HasValue) entty.StandardTime = model.standartTime.Value;

        await _context.SaveChangesAsync();

        return entty.Id;
    }

    public async Task<long> Delete(long id)
    {
        var entoty = await _context.Works
            .Where(w => w.Id == id)
            .ExecuteDeleteAsync();

        return id;
    }

    public async Task<bool> Exists(long id)
    {
        return await _context.Works
            .AsNoTracking()
            .AnyAsync(w => w.Id == id);
    }
}
