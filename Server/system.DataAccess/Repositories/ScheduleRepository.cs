using CRMSystem.Core.DTOs.Schedule;
using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;

namespace CRMSystem.DataAccess.Repositories;

public class ScheduleRepository : IScheduleRepository
{
    private readonly SystemDbContext _context;

    public ScheduleRepository(SystemDbContext context)
    {
        _context = context;
    }

    private IQueryable<ScheduleEntity> ApplyFilter(IQueryable<ScheduleEntity> query, ScheduleFilter filter)
    {
        if (filter.WorkerIds != null && filter.WorkerIds.Any())
            query = query.Where(s => filter.WorkerIds.Contains(s.WorkerId));

        if (filter.ShiftIds != null && filter.ShiftIds.Any())
            query = query.Where(s => filter.ShiftIds.Contains(s.ShiftId));

        return query;
    }

    public async Task<List<ScheduleItem>> GetPaged(ScheduleFilter filter)
    {
        var query = _context.Schedules.AsNoTracking();
        query = ApplyFilter(query, filter);

        query = filter.SortBy?.ToLower().Trim() switch
        {
            "worker" => filter.IsDescending
                ? query.OrderByDescending(s => s.Worker == null
                    ? string.Empty
                    : s.Worker.Name)
                : query.OrderBy(s => s.Worker == null
                    ? string.Empty
                    : s.Worker.Name),
            "shift" => filter.IsDescending
                ? query.OrderByDescending(s => s.Shift == null
                    ? string.Empty
                    : s.Shift.Name)
                : query.OrderByDescending(s => s.Shift == null
                    ? string.Empty
                    : s.Shift.Name),
            "datetime" => filter.IsDescending
                ? query.OrderByDescending(s => s.Date)
                : query.OrderBy(s => s.Date),

            _ => filter.IsDescending
                ? query.OrderByDescending(s => s.Id)
                : query.OrderBy(s => s.Id)
        };

        var projection = query.Select(s => new ScheduleItem(
            s.Id,
            s.Worker == null
                ? string.Empty
                : $"{s.Worker.Name} {s.Worker.Surname}",
            s.WorkerId,
            s.Shift == null
                ? string.Empty
                : s.Shift.Name,
            s.ShiftId,
            s.Date));

        return await projection
            .Skip((filter.Page - 1) * filter.Limit)
            .Take(filter.Limit)
            .ToListAsync();
    }

    public async Task<int> GetCount(ScheduleFilter filter)
    {
        var query = _context.Schedules.AsNoTracking();
        query = ApplyFilter(query, filter);
        return await query.CountAsync();
    }

    public async Task<int> Create(Schedule schedule)
    {
        var scheduleEntity = new ScheduleEntity
        {
            WorkerId = schedule.WorkerId,
            ShiftId = schedule.ShiftId,
            Date = schedule.Date,
        };

        await _context.Schedules.AddAsync(scheduleEntity);
        await _context.SaveChangesAsync();

        return scheduleEntity.Id;
    }

    public async Task<int> Update(int id, ScheduleUpdateModel model)
    {
        var entity = await _context.Schedules.FirstOrDefaultAsync(s => s.Id == id)
            ?? throw new Exception("Schedule note not found");

        if (model.ShiftId.HasValue) entity.ShiftId = model.ShiftId.Value;
        if (model.DateTime.HasValue) entity.Date = model.DateTime.Value;

        await _context.SaveChangesAsync();

        return entity.Id;
    }

    public async Task<int> Delete(int id)
    {
        var entity = await _context.Schedules
            .Where(s => s.Id == id)
            .ExecuteDeleteAsync();

        return id;
    }

    public async Task<bool> ExistsByDateAndId(int id, DateTime date)
    {
        return await _context.Schedules
            .AsNoTracking()
            .Where(s => s.Id == id)
            .AnyAsync(s => s.Date == date);
    }
}
