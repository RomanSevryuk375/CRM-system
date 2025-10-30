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

    public async Task<List<Worker>> Get()
    {
        var workerEntities = await _context.Workers
            .AsNoTracking()
            .ToListAsync();

        var worker = workerEntities
            .Select(w => Worker.Create(
                w.Id,
                w.UserId,
                w.SpecializationId,
                w.Name,
                w.Surname,
                w.HourlyRate,
                w.PhoneNumber,
                w.Email).worker)
            .ToList();

        return worker;
    }

    public async Task<int> Create(Worker worker)
    {
        var (_, error) = Worker.Create(
            0,
            worker.UserId,
            worker.SpecializationId,
            worker.Name,
            worker.Surname,
            worker.HourlyRate,
            worker.PhoneNumber,
            worker.Email);

        if (!string.IsNullOrEmpty(error))
            throw new ArgumentException($"Create exception Worker: {error}");

        var workerEntities = new WorkerEntity
        {
            UserId = worker.UserId,
            SpecializationId = worker.SpecializationId,
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

    public async Task<int> Update(
        int id,
        int? userId,
        int? specialization,
        string name,
        string Surname,
        decimal? hourlyRate,
        string phoneNumber,
        string email)
    {
        var workerEntity = await _context.Workers.FirstOrDefaultAsync(w => w.Id == id)
            ?? throw new Exception("Payment note not found");

        if (userId.HasValue)
            workerEntity.UserId = userId.Value;
        if (specialization.HasValue)
            workerEntity.SpecializationId = specialization.Value;
        if (!string.IsNullOrEmpty(name))
            workerEntity.Name = name;
        if (!string.IsNullOrEmpty(Surname))
            workerEntity.Surname = Surname;
        if (hourlyRate.HasValue)
            workerEntity.HourlyRate = hourlyRate.Value;
        if (!string.IsNullOrEmpty(phoneNumber))
            workerEntity.PhoneNumber = phoneNumber;
        if (!string.IsNullOrEmpty(email))
            workerEntity.Email = email;

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
}
