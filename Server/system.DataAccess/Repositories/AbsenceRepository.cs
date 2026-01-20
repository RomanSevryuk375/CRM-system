using AutoMapper;
using AutoMapper.QueryableExtensions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels.Absence;
using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;
using CRMSystem.Core.Exceptions;
using Shared.Enums;

namespace CRMSystem.DataAccess.Repositories;

public class AbsenceRepository : IAbsenceRepository
{
    private readonly SystemDbContext _context;
    private readonly IMapper _mapper;

    public AbsenceRepository(
        SystemDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    private static IQueryable<AbsenceEntity> ApplyFilter(IQueryable<AbsenceEntity> query, AbsenceFilter filter)
    {
        if (filter.WorkerIds != null && filter.WorkerIds.Any())
            query = query.Where(x => filter.WorkerIds.Contains(x.WorkerId));

        return query;
    }

    public async Task<List<AbsenceItem>> GetPaged(AbsenceFilter filter, CancellationToken ct)
    {
        var query = _context.Absences.AsNoTracking();
        query = ApplyFilter(query, filter);

        query = filter.SortBy?.ToLower().Trim() switch
        {
            "type" => filter.IsDescending 
                ? query.OrderByDescending(a => a.AbsenceType == null 
                    ? string.Empty 
                    : a.AbsenceType.Name) 
                : query.OrderBy(a => a.AbsenceType == null 
                    ? string.Empty 
                    : a.AbsenceType.Name),
            "startdate" => filter.IsDescending 
                ? query.OrderByDescending(a => a.StartDate) 
                : query.OrderBy(a => a.StartDate),
            "enddate" => filter.IsDescending 
                ? query.OrderByDescending(a => a.EndDate) 
                : query.OrderBy(a => a.EndDate),

            _ => filter.IsDescending 
            ? query.OrderByDescending(a => a.Id) 
            : query.OrderBy(a => a.Id)
        };

        return await query
            .ProjectTo<AbsenceItem>(_mapper.ConfigurationProvider, ct)
            .Skip((filter.Page - 1) * filter.Limit)
            .Take(filter.Limit)
            .ToListAsync(ct);
    }

    public async Task<int> GetCount(AbsenceFilter filter, CancellationToken ct)
    {
        var query = _context.Absences.AsNoTracking();
        query = ApplyFilter(query, filter);
        return await query.CountAsync(ct);
    }

    public async Task<int> Create(Absence absence, CancellationToken ct)
    {
        var absenceEntity = new AbsenceEntity
        {
            WorkerId = absence.WorkerId,
            TypeId = (int)absence.TypeId,
            StartDate = absence.StartDate,
            EndDate = absence.EndDate
        };

        await _context.Absences.AddAsync(absenceEntity, ct);
        await _context.SaveChangesAsync(ct);

        return absenceEntity.Id;
    }

    public async Task<int> Update(int id, AbsenceUpdateModel model, CancellationToken ct)
    {
        var entity = await _context.Absences.FirstOrDefaultAsync(a => a.Id == id, ct)
            ?? throw new NotFoundException("Absence not found");

        if (model.TypeId.HasValue) entity.TypeId = (int)model.TypeId.Value;
        if (model.StartDate.HasValue) entity.StartDate = model.StartDate.Value;
        if (model.EndDate.HasValue) entity.EndDate = model.EndDate.Value;

        await _context.SaveChangesAsync(ct);
        return entity.Id;
    }

    public async Task<int> Delete(int id, CancellationToken ct)
    {
        var entity = await _context.Absences
            .Where(a => a.Id == id)
            .ExecuteDeleteAsync(ct);

        return id;
    }

    public async Task<bool> Exists(int id, CancellationToken ct)
    {
        return await _context.Absences
            .AsNoTracking()
            .AnyAsync(a => a.Id == id, ct);        
    }

    public async Task<int?> GetWorkerId(int id, CancellationToken ct)
    {
        return await _context.Absences
            .Where(a => a.Id == id)
            .Select(a => a.WorkerId)
            .FirstOrDefaultAsync(ct);
    }

    public async Task<List<Absence?>> GetByWorkerId(int workerId, CancellationToken ct)
    {
        return await _context.Absences
            .Where(a => a.WorkerId == workerId)
            .Select(a => Absence.Create(a.Id, a.WorkerId, (AbsenceTypeEnum)a.TypeId, a.StartDate, a.EndDate).absence)
            .ToListAsync(ct);
    }
}
