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
        if (filter.workerIds != null && filter.workerIds.Any())
            query = query.Where(x => filter.workerIds.Contains(x.WorkerId));

        if (filter.orderIds != null && filter.orderIds.Any())
            query = query.Where(x => filter.orderIds.Contains(x.OrderId));

        return query;
    }

    public async Task<List<AcceptanceItem>> GetPaged(AcceptanceFilter filter)
    {
        var query = _context.Acceptances.AsNoTracking();
        query = ApplyFilter(query, filter);

        query = filter.SortBy?.ToLower().Trim() switch
        {
            "createdAt" => filter.isDescending
                ? query.OrderByDescending(a => a.CreateAt)
                : query.OrderBy(a => a.CreateAt),
            "mileage" => filter.isDescending
                ? query.OrderByDescending(a => a.Mileage)
                : query.OrderBy(a => a.Mileage),
            "fuelLevel" => filter.isDescending
                ? query.OrderByDescending(a => a.FuelLevel)
                : query.OrderBy(a => a.FuelLevel),
            "externalDefects" => filter.isDescending
                ? query.OrderByDescending(a => a.ExternalDefects)
                : query.OrderBy(a => a.ExternalDefects),
            "internalDefects" => filter.isDescending
                ? query.OrderByDescending(a => a.InternalDefects)
                : query.OrderBy(a => a.InternalDefects),
            "clientSign" => filter.isDescending
                ? query.OrderByDescending(a => a.ClientSign)
                : query.OrderBy(a => a.ClientSign),
            "workerSign" => filter.isDescending
                ? query.OrderByDescending(a => a.WorkerSign)
                : query.OrderBy(a => a.WorkerSign),

            _ => filter.isDescending
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

        if (model.mileage.HasValue) entity.Mileage = model.mileage.Value;
        if (model.fuelLevel.HasValue) entity.FuelLevel = model.fuelLevel.Value;
        if (!string.IsNullOrEmpty(model.externalDefects)) entity.ExternalDefects = model.externalDefects;
        if (!string.IsNullOrEmpty(model.internalDefects)) entity.InternalDefects = model.internalDefects;
        if (model.clientSign.HasValue) entity.ClientSign = model.clientSign.Value;
        if (model.workerSign.HasValue) entity.WorkerSign = model.workerSign.Value;

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
}
