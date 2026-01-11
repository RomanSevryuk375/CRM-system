using AutoMapper;
using AutoMapper.QueryableExtensions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels.Worker;
using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;
using CRMSystem.Core.Exceptions;

namespace CRMSystem.DataAccess.Repositories;

public class WorkerRepository : IWorkerRepository
{
    private readonly SystemDbContext _context;
    private readonly IMapper _mapper;

    public WorkerRepository(
        SystemDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    private static IQueryable<WorkerEntity> ApplyFilter(IQueryable<WorkerEntity> query, WorkerFilter filter)
    {
        if (filter.WorkerIds != null && filter.WorkerIds.Any())
            query = query.Where(w => filter.WorkerIds.Contains(w.Id));

        return query;
    }

    public async Task<List<WorkerItem>> GetPaged(WorkerFilter filter, CancellationToken ct)
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

        return await query
            .ProjectTo<WorkerItem>(_mapper.ConfigurationProvider, ct)
            .Skip((filter.Page - 1) * filter.Limit)
            .Take(filter.Limit)
            .ToListAsync(ct);
    }

    public async Task<WorkerItem?> GetById(int id, CancellationToken ct)
    {
        return await _context.Workers
            .AsNoTracking()
            .Where(w => w.Id == id)
            .ProjectTo<WorkerItem>(_mapper.ConfigurationProvider, ct)
            .FirstOrDefaultAsync(ct);
    }

    public async Task<int> GetCount(WorkerFilter filter, CancellationToken ct)
    {
        var query = _context.Workers.AsNoTracking();
        query = ApplyFilter(query, filter);
        return await query.CountAsync(ct);
    }

    public async Task<int> Create(Worker worker, CancellationToken ct)
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

        await _context.Workers.AddAsync(workerEntities, ct);
        await _context.SaveChangesAsync(ct);

        return worker.Id;
    }

    public async Task<int> Update(int id, WorkerUpdateModel model, CancellationToken ct)
    {
        var workerEntity = await _context.Workers.FirstOrDefaultAsync(w => w.Id == id, ct)
            ?? throw new NotFoundException("Payment note not found");

        if (!string.IsNullOrWhiteSpace(model.Name)) workerEntity.Name = model.Name;
        if (!string.IsNullOrWhiteSpace(model.Surname)) workerEntity.Surname = model.Surname;
        if (model.HourlyRate.HasValue) workerEntity.HourlyRate = model.HourlyRate.Value;
        if (!string.IsNullOrWhiteSpace(model.PhoneNumber)) workerEntity.PhoneNumber = model.PhoneNumber;
        if (!string.IsNullOrWhiteSpace(model.Email)) workerEntity.Email = model.Email;

        await _context.SaveChangesAsync(ct);

        return workerEntity.Id;
    }

    public async Task<int> Delete(int id, CancellationToken ct)
    {
        var worker = await _context.Workers
            .Where(w => w.Id == id)
            .ExecuteDeleteAsync(ct);

        return id;
    }

    public async Task<bool> Exists(int id, CancellationToken ct)
    {
        var exist = await _context.Workers
            .AsNoTracking()
            .AnyAsync(a => a.Id == id, ct);

        return exist;
    }
}
