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
                ? query.OrderByDescending(a => a.AbsenceType == null ? string.Empty : a.AbsenceType.Name) 
                : query.OrderBy(a => a.AbsenceType == null ? string.Empty : a.AbsenceType.Name),
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
            a.Worker == null ? "" : $"{a.Worker.Name} {a.Worker.Surname}",
            a.AbsenceType == null ? string.Empty : a.AbsenceType.Name,
            a.StartDate,
            a.EndDate));

        return await projection
            .Skip((filter.Page - 1) * filter.Limit)
            .Take(filter.Limit)
            .ToListAsync();
    }

    public async Task<long> GetCount(AbsenceFilter filter)
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
            TypeId = absence.TypeId,
            StartDate = absence.StartDate,
            EndDate = absence.EndDate
        };

        await _context.Absences.AddAsync(absenceEntity);
        await _context.SaveChangesAsync();

        return absenceEntity.Id;
    }

    public async Task<int> Update(AbsenceUpdateModel model)
    {
        var entity = await _context.Absences.FirstOrDefaultAsync(a => a.Id == model.id);

        if (entity == null) throw new Exception("Absences not found");

        if (model.typeId.HasValue) entity.TypeId = model.typeId.Value;
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
}
