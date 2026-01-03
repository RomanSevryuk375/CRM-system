using CRMSystem.Core.DTOs.Worker;
using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;

namespace CRMSystem.DataAccess.Repositories;

public class WorkerRepository : IWorkerRepository
{
    private readonly SystemDbContext _context;

    public WorkerRepository(SystemDbContext context)
    {
        _context = context;
    }

    private IQueryable<WorkerEntity> ApplyFilter(IQueryable<WorkerEntity> query, WorkerFilter filter)
    {
        if (filter.WorkerIds != null && filter.WorkerIds.Any())
            query = query.Where(w => filter.WorkerIds.Contains(w.Id));

        return query;
    }

    public async Task<List<WorkerItem>> GetPaged(WorkerFilter filter)
    {
        var query = _context.Workers.AsNoTracking();
        query = ApplyFilter(query, filter);

        query = filter.SortBy?.ToLower().Trim() switch
        {
            "name" => filter.IsDescending
                ? query.OrderByDescending(w => w.Name)
                : query.OrderBy(w => w.Name),
            "surname" => filter.IsDescending
                ? query.OrderByDescending(w => w.Surname)
                : query.OrderBy(w => w.Surname),
            "phonenumber" => filter.IsDescending
                ? query.OrderByDescending(w => w.PhoneNumber)
                : query.OrderBy(w => w.PhoneNumber),
            "email" => filter.IsDescending
                ? query.OrderByDescending(w => w.Email)
                : query.OrderBy(w => w.Email),
            "hourlyrate" => filter.IsDescending
                ? query.OrderByDescending(w => w.HourlyRate)
                : query.OrderBy(w => w.HourlyRate),

            _ => filter.IsDescending
                ? query.OrderByDescending(w => w.Id)
                : query.OrderBy(w => w.Id),
        };

        var projection = query.Select(c => new WorkerItem(
            c.Id,
            c.UserId,
            c.Name,
            c.Surname,
            c.HourlyRate,
            c.PhoneNumber,
            c.Email));

        return await projection
            .Skip((filter.Page - 1) * filter.Limit)
            .Take(filter.Limit)
            .ToListAsync();
    }

    public async Task<WorkerItem?> GetById(int id)
    {
        return await _context.Workers
            .AsNoTracking()
            .Where(w => w.Id == id)
            .Select(c => new WorkerItem(
            c.Id,
            c.UserId,
            c.Name,
            c.Surname,
            c.HourlyRate,
            c.PhoneNumber,
            c.Email))
            .FirstOrDefaultAsync();
    }

    public async Task<int> GetCount(WorkerFilter filter)
    {
        var query = _context.Workers.AsNoTracking();
        query = ApplyFilter(query, filter);
        return await query.CountAsync();
    }

    public async Task<int> Create(Worker worker)
    {
        var workerEntities = new WorkerEntity
        {
            UserId = worker.UserId,
            Name = worker.Name,
            Surname = worker.Surname,
            HourlyRate = worker.HourlyRate,
            PhoneNumber = worker.PhoneNumber,
            Email = worker.Email
        };

        await _context.Workers.AddAsync(workerEntities);
        await _context.SaveChangesAsync();

        return worker.Id;
    }

    public async Task<int> Update(int id, WorkerUpdateModel model)
    {
        var workerEntity = await _context.Workers.FirstOrDefaultAsync(w => w.Id == id)
            ?? throw new Exception("Payment note not found");

        if (!string.IsNullOrWhiteSpace(model.Name)) workerEntity.Name = model.Name;
        if (!string.IsNullOrWhiteSpace(model.Surname)) workerEntity.Surname = model.Surname;
        if (model.HourlyRate.HasValue) workerEntity.HourlyRate = model.HourlyRate.Value;
        if (!string.IsNullOrWhiteSpace(model.PhoneNumber)) workerEntity.PhoneNumber = model.PhoneNumber;
        if (!string.IsNullOrWhiteSpace(model.Email)) workerEntity.Email = model.Email;

        await _context.SaveChangesAsync();

        return workerEntity.Id;
    }

    public async Task<int> Delete(int id)
    {
        var worker = await _context.Workers
            .Where(w => w.Id == id)
            .ExecuteDeleteAsync();

        return id;
    }

    public async Task<bool> Exists(int id)
    {
        var exist = await _context.Workers
            .AsNoTracking()
            .AnyAsync(a => a.Id == id);

        return exist;
    }
}
