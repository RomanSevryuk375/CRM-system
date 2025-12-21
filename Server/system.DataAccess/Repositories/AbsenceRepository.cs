using CRMSystem.Core.DTOs.Absence;
using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;

namespace CRMSystem.DataAccess.Repositories;

public class AbsenceRepository : IAbsenceRepository
{
    private readonly SystemDbContext _context;

    public AbsenceRepository(SystemDbContext context)
    {
        _context = context;
    }

    private IQueryable<AbsenceEntity> ApplyFilter(IQueryable<AbsenceEntity> query, AbsenceFilter filter)
    {
        if (filter.workerIds != null && filter.workerIds.Any())
            query = query.Where(x => filter.workerIds.Contains(x.WorkerId));

        return query;
    }

    public async Task<List<AbsenceItem>> GetPaged(AbsenceFilter filter)
    {
        var query = _context.Absences.AsNoTracking();
        query = ApplyFilter(query, filter);

        query = filter.SortBy?.ToLower().Trim() switch
        {
            "type" => filter.isDescending 
                ? query.OrderByDescending(a => a.AbsenceType == null 
                    ? string.Empty 
                    : a.AbsenceType.Name) 
                : query.OrderBy(a => a.AbsenceType == null 
                    ? string.Empty 
                    : a.AbsenceType.Name),
            "startdate" => filter.isDescending 
                ? query.OrderByDescending(a => a.StartDate) 
                : query.OrderBy(a => a.StartDate),
            "enddate" => filter.isDescending 
                ? query.OrderByDescending(a => a.EndDate) 
                : query.OrderBy(a => a.EndDate),

            _ => filter.isDescending 
            ? query.OrderByDescending(a => a.Id) 
            : query.OrderBy(a => a.Id)
        };

        var projection = query.Select(a => new AbsenceItem(
            a.Id,
            a.Worker == null 
                ? string.Empty 
                : $"{a.Worker.Name} {a.Worker.Surname}",
            a.WorkerId,
            a.AbsenceType == null 
                ? string.Empty 
                : a.AbsenceType.Name,
            a.StartDate,
            a.EndDate));

        return await projection
            .Skip((filter.Page - 1) * filter.Limit)
            .Take(filter.Limit)
            .ToListAsync();
    }

    public async Task<int> GetCount(AbsenceFilter filter)
    {
        var query = _context.Absences.AsNoTracking();
        query = ApplyFilter(query, filter);
        return await query.CountAsync();
    }

    public async Task<int> Create(Absence absence)
    {
        var absenceEntity = new AbsenceEntity
        {
            WorkerId = absence.WorkerId,
            TypeId = (int)absence.TypeId,
            StartDate = absence.StartDate,
            EndDate = absence.EndDate
        };

        await _context.Absences.AddAsync(absenceEntity);
        await _context.SaveChangesAsync();

        return absenceEntity.Id;
    }

    public async Task<int> Update(int id, AbsenceUpdateModel model)
    {
        var entity = await _context.Absences.FirstOrDefaultAsync(a => a.Id == id)
            ?? throw new Exception("Absence not found");

        if (model.typeId.HasValue) entity.TypeId = (int)model.typeId.Value;
        if (model.startDate.HasValue) entity.StartDate = model.startDate.Value;
        if (model.endDate.HasValue) entity.EndDate = model.endDate.Value;

        await _context.SaveChangesAsync();
        return entity.Id;
    }

    public async Task<int> Delete(int id)
    {
        var entity = await _context.Absences
            .Where(a => a.Id == id)
            .ExecuteDeleteAsync();

        return id;
    }

    public async Task<bool> Exists(int id)
    {
        return await _context.Absences
            .AsNoTracking()
            .AnyAsync(a => a.Id == id);        
    }


    public async Task<bool> HasOverLap(int workerId, DateOnly start, DateOnly? end, int? excludeId = null)
    {
        var query = _context.Absences.AsNoTracking()
            .Where(a => a.WorkerId == workerId);

        if (excludeId.HasValue)
            query = query.Where(a => a.Id != excludeId.Value);

        return await query.AnyAsync(a =>
                start <= (a.EndDate ?? DateOnly.MaxValue)
                &&
                a.StartDate <= (end ?? DateOnly.MaxValue));
    }
}
