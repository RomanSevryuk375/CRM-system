using CRMSystem.Core.DTOs.Acceptance;
using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;

namespace CRMSystem.DataAccess.Repositories;

public class AcceptanceRepository : IAcceptanceRepository
{
    private readonly SystemDbContext _context;

    public AcceptanceRepository(SystemDbContext context)
    {
        _context = context;
    }

    private IQueryable<AcceptanceEntity> ApplyFilter(IQueryable<AcceptanceEntity> query, AcceptanceFilter filter)
    {
        if (filter.WorkerIds != null && filter.WorkerIds.Any())
            query = query.Where(x => filter.WorkerIds.Contains(x.WorkerId));

        if (filter.OrderIds != null && filter.OrderIds.Any())
            query = query.Where(x => filter.OrderIds.Contains(x.OrderId));

        if (filter.AcceptanceIds != null && filter.AcceptanceIds.Any())
            query = query.Where(x => filter.AcceptanceIds.Contains(x.Id));

        return query;
    }

    public async Task<List<AcceptanceItem>> GetPaged(AcceptanceFilter filter)
    {
        var query = _context.Acceptances.AsNoTracking();
        query = ApplyFilter(query, filter);

        query = filter.SortBy?.ToLower().Trim() switch
        {
            "createdAt" => filter.IsDescending
                ? query.OrderByDescending(a => a.CreateAt)
                : query.OrderBy(a => a.CreateAt),
            "mileage" => filter.IsDescending
                ? query.OrderByDescending(a => a.Mileage)
                : query.OrderBy(a => a.Mileage),
            "fuelLevel" => filter.IsDescending
                ? query.OrderByDescending(a => a.FuelLevel)
                : query.OrderBy(a => a.FuelLevel),
            "externalDefects" => filter.IsDescending
                ? query.OrderByDescending(a => a.ExternalDefects)
                : query.OrderBy(a => a.ExternalDefects),
            "internalDefects" => filter.IsDescending
                ? query.OrderByDescending(a => a.InternalDefects)
                : query.OrderBy(a => a.InternalDefects),
            "clientSign" => filter.IsDescending
                ? query.OrderByDescending(a => a.ClientSign)
                : query.OrderBy(a => a.ClientSign),
            "workerSign" => filter.IsDescending
                ? query.OrderByDescending(a => a.WorkerSign)
                : query.OrderBy(a => a.WorkerSign),

            _ => filter.IsDescending
                ? query.OrderByDescending(a => a.Id)
                : query.OrderBy(a => a.Id),
        };

        var projection = query.Select(a => new AcceptanceItem(
            a.Id,
            a.OrderId,
            a.Worker == null 
                ? string.Empty 
                : $"{a.Worker.Name} {a.Worker.Surname}",
            a.WorkerId,
            a.CreateAt,
            a.Mileage,
            a.FuelLevel,
            a.ExternalDefects,
            a.InternalDefects,
            a.ClientSign,
            a.WorkerSign));

        return await projection
            .Skip((filter.Page - 1) * filter.Limit)
            .Take(filter.Limit)
            .ToListAsync();
    }

    public async Task<int> GetCount(AcceptanceFilter filter)
    {
        var query = _context.Acceptances.AsNoTracking();
        query = ApplyFilter(query, filter);
        return await query.CountAsync();
    }

    public async Task<long> Create(Acceptance acceptance)
    {
        var accptanceEntity = new AcceptanceEntity
        {
            OrderId = acceptance.OrderId,
            WorkerId = acceptance.WorkerId,
            CreateAt = acceptance.CreateAt,
            Mileage = acceptance.Mileage,
            FuelLevel = acceptance.FuelLevel,
            ExternalDefects = acceptance.ExternalDefects,
            InternalDefects = acceptance.InternalDefects,
            ClientSign = acceptance.ClientSign,
            WorkerSign = acceptance.WorkerSign
        };

        await _context.AddAsync(accptanceEntity);
        await _context.SaveChangesAsync();

        return accptanceEntity.Id;
    }

    public async Task<long> Update(long id, AcceptanceUpdateModel model)
    {
        var entity = await _context.Acceptances.FirstOrDefaultAsync(a => a.Id == id)
            ?? throw new Exception("Acceptance not found");

        if (model.Mileage.HasValue) entity.Mileage = model.Mileage.Value;
        if (model.FuelLevel.HasValue) entity.FuelLevel = model.FuelLevel.Value;
        if (!string.IsNullOrEmpty(model.ExternalDefects)) entity.ExternalDefects = model.ExternalDefects;
        if (!string.IsNullOrEmpty(model.InternalDefects)) entity.InternalDefects = model.InternalDefects;
        if (model.ClientSign.HasValue) entity.ClientSign = model.ClientSign.Value;
        if (model.WorkerSign.HasValue) entity.WorkerSign = model.WorkerSign.Value;

        await _context.SaveChangesAsync();

        return entity.Id;
    }

    public async Task<long> Delete(long id)
    {
        var entity = await _context.Acceptances
            .Where(a => a.Id == id)
            .ExecuteDeleteAsync();

        return id;
    }

    public async Task<bool> Exists(long id)
    {
        return await _context.Acceptances
            .AsNoTracking()
            .AnyAsync(a => a.Id == id);
    }
}
