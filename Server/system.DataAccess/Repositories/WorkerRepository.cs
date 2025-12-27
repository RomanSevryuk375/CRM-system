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
        if (filter.workerIds != null && filter.workerIds.Any())
            query = query.Where(w => filter.workerIds.Contains(w.Id));

        return query;
    }

    public async Task<List<WorkerItem>> GetPaged(WorkerFilter filter)
    {
        var query = _context.Workers.AsNoTracking();
        query = ApplyFilter(query, filter);

        query = filter.SortBy?.ToLower().Trim() switch
        {
            "name" => filter.isDescending
                ? query.OrderByDescending(w => w.Name)
                : query.OrderBy(w => w.Name),
            "surname" => filter.isDescending
                ? query.OrderByDescending(w => w.Surname)
                : query.OrderBy(w => w.Surname),
            "phonenumber" => filter.isDescending
                ? query.OrderByDescending(w => w.PhoneNumber)
                : query.OrderBy(w => w.PhoneNumber),
            "email" => filter.isDescending
                ? query.OrderByDescending(w => w.Email)
                : query.OrderBy(w => w.Email),
            "hourlyrate" => filter.isDescending
                ? query.OrderByDescending(w => w.HourlyRate)
                : query.OrderBy(w => w.HourlyRate),

            _ => filter.isDescending
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

        if (!string.IsNullOrWhiteSpace(model.name)) workerEntity.Name = model.name;
        if (!string.IsNullOrWhiteSpace(model.surname)) workerEntity.Surname = model.surname;
        if (model.hourlyRate.HasValue) workerEntity.HourlyRate = model.hourlyRate.Value;
        if (!string.IsNullOrWhiteSpace(model.phoneNumber)) workerEntity.PhoneNumber = model.phoneNumber;
        if (!string.IsNullOrWhiteSpace(model.email)) workerEntity.Email = model.email;

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
