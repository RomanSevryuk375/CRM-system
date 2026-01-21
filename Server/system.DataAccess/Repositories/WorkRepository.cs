using AutoMapper;
using AutoMapper.QueryableExtensions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels.Work;
using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;
using CRMSystem.Core.Exceptions;
using Shared.Filters;

namespace CRMSystem.DataAccess.Repositories;

public class WorkRepository : IWorkRepository
{
    private readonly SystemDbContext _context;
    private readonly IMapper _mapper;

    public WorkRepository(
        SystemDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<WorkItem>> GetPaged(WorkFilter filter, CancellationToken ct)
    {
        var query = _context.Works.AsNoTracking();

        query = filter.SortBy?.ToLower().Trim() switch
        {
            "title" => filter.IsDescending
                ? query.OrderByDescending(w => w.Title)
                : query.OrderBy(w => w.Title),
            "category" => filter.IsDescending
                ? query.OrderByDescending(w => w.Category)
                : query.OrderBy(w => w.Category),
            "description" => filter.IsDescending
                ? query.OrderByDescending(w => w.Description)
                : query.OrderBy(w => w.Description),
            "standarttime" => filter.IsDescending
                ? query.OrderByDescending(w => w.StandardTime)
                : query.OrderBy(w => w.StandardTime),


            _ => filter.IsDescending
                ? query.OrderByDescending(w => w.Id)
                : query.OrderBy(w => w.Id),
        };

        return await query
            .ProjectTo<WorkItem>(_mapper.ConfigurationProvider, ct)
            .Skip((filter.Page - 1) * filter.Limit)
            .Take(filter.Limit)
            .ToListAsync(ct);
    }

    public async Task<WorkItem?> GetById (long id, CancellationToken ct)
    {
        return await _context.Works
            .AsNoTracking()
            .Where(w => w.Id == id)
            .ProjectTo<WorkItem>(_mapper.ConfigurationProvider, ct)
            .FirstOrDefaultAsync(ct);

    }

    public async Task<int> GetCount(CancellationToken ct)
    {
        return await _context.Works.CountAsync(ct);
    }

    public async Task<long> Create(Work work, CancellationToken ct)
    {
        var workEntity = new WorkEntity
        {
            Title = work.Title,
            Category = work.Category,
            Description = work.Description,
            StandardTime = work.StandardTime,
        };

        await _context.Works.AddAsync(workEntity, ct);
        await _context.SaveChangesAsync(ct);

        return workEntity.Id;
    }

    public async Task<long> Update(long id, WorkUpdateModel model, CancellationToken ct)
    {
        var entty = await _context.Works.FirstOrDefaultAsync(w => w.Id == id, ct)
            ?? throw new NotFoundException("Work not found");

        if (!string.IsNullOrWhiteSpace(model.Title)) entty.Title = model.Title;
        if (!string.IsNullOrWhiteSpace(model.Categoty)) entty.Category = model.Categoty;
        if (!string.IsNullOrWhiteSpace(model.Description)) entty.Description = model.Description;
        if (model.StandartTime.HasValue) entty.StandardTime = model.StandartTime.Value;

        await _context.SaveChangesAsync(ct);

        return entty.Id;
    }

    public async Task<long> Delete(long id, CancellationToken ct)
    {
        var entoty = await _context.Works
            .Where(w => w.Id == id)
            .ExecuteDeleteAsync(ct);

        return id;
    }

    public async Task<bool> Exists(long id, CancellationToken ct)
    {
        return await _context.Works
            .AsNoTracking()
            .AnyAsync(w => w.Id == id, ct);
    }
}
