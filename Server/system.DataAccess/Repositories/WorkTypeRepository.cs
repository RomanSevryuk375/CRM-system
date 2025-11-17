using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;

namespace CRMSystem.DataAccess.Repositories;

public class WorkTypeRepository : IWorkTypeRepository
{
    private readonly SystemDbContext _context;

    public WorkTypeRepository(SystemDbContext context)
    {
        _context = context;
    }

    public async Task<List<WorkType>> Get()
    {

        var workTypeEntities = await _context.WorkTypes
            .AsNoTracking()
            .ToListAsync();

        var workTypes = workTypeEntities
            .Select(wt => WorkType.Create(
                wt.Id,
                wt.Title,
                wt.Category,
                wt.Description,
                wt.StandardTime).workType)
            .ToList();

        return workTypes;
    }

    public async Task<int> GetCount()
    {
        return await _context.WorkTypes.CountAsync();
    }

    public async Task<int> Create(WorkType workType)
    {
        var (_, error) = WorkType.Create(
            0, 
            workType.Title,
            workType.Category,
            workType.Description, 
            workType.StandardTime);
        if (!string.IsNullOrEmpty(error))
            throw new ArgumentException($"Create exception WorkType: {error}");

        var workEntities = new WorkTypeEntity
        {
            Title = workType.Title,
            Category = workType.Category,
            Description = workType.Description,
            StandardTime = workType.StandardTime,
        };

        await _context.WorkTypes.AddAsync(workEntities);
        await _context.SaveChangesAsync();

        return workType.Id;
    }

    public async Task<int> Update(int id, string? title, string? category, string? description, decimal? standardTime)
    {
        var workType = await _context.WorkTypes.FirstOrDefaultAsync(wt => wt.Id == id)
            ?? throw new Exception("Work type not found");

        if (!string.IsNullOrWhiteSpace(title))
            workType.Title = title;
        if (!string.IsNullOrWhiteSpace(category))
            workType.Category = category;
        if (!string.IsNullOrWhiteSpace(description))
            workType.Description = description;
        if (standardTime.HasValue)
            workType.StandardTime = standardTime.Value;

        await _context.SaveChangesAsync();

        return workType.Id;
    }

    public async Task<int> Delete(int id)
    {
        var workTypeEntities = await _context.WorkTypes
            .Where(wt => wt.Id == id)
            .ExecuteDeleteAsync();

        return id;
    }
}
