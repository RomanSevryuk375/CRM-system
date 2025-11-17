using CRMSystem.Buisnes.DTOs;
using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Repositories;

namespace CRMSystem.Buisnes.Services;

public class WorkerService : IWorkerService
{
    private readonly IWorkerRepository _workerRepository;
    private readonly ISpecializationRepository _specializationRepository;

    public WorkerService(IWorkerRepository workerRepository, ISpecializationRepository specializationRepository)
    {
        _workerRepository = workerRepository;
        _specializationRepository = specializationRepository;
    }

    public async Task<List<Worker>> GetPagedAllWorkers(int page, int limit)
    {
        return await _workerRepository.GetPaged(page, limit);
    }

    public async Task<int> GetCountWorker()
    {
        return await _workerRepository.GetCount();
    }

    public async Task<List<WorkerWithInfoDto>> GetPagedWorkerByUserId(int userId, int page, int limit)
    {
        var worker = await _workerRepository.GetWorkerByUserId(userId);
        var specializations = await _specializationRepository.GetPaged(page, limit);

        var response = (from w in worker
                        join s in specializations on w.SpecializationId equals s.Id
                        select new WorkerWithInfoDto(
                            w.Id,
                            w.UserId,
                            s.Name,
                            w.Name,
                            w.Surname,
                            w.HourlyRate,
                            w.PhoneNumber,
                            w.Email
                            )).ToList();

        return response;
    }

    public async Task<int> GetCountWorkerByUserId(int userId)
    {
        return await _workerRepository.GetCountWorkerByUserId(userId);
    }

    public async Task<List<WorkerWithInfoDto>> GetPagedWorkersWithInfo(int page, int limit)
    {
        var workers = await _workerRepository.GetPaged(page, limit);
        var specializations = await _specializationRepository.Get();

        var response = (from w in workers
                        join s in specializations on w.SpecializationId equals s.Id
                        select new WorkerWithInfoDto(
                            w.Id,
                            w.UserId,
                            s.Name,
                            w.Name,
                            w.Surname,
                            w.HourlyRate,
                            w.PhoneNumber,
                            w.Email
                            )).ToList();

        return response;
    }

    public async Task<int> CreateWorker(Worker worker)
    {
        return await _workerRepository.Create(worker);
    }

    public async Task<int> UpdateWorker(int id, int? userId, int? specialization, string name, string Surname, decimal? hourlyRate, string phoneNumber, string email)
    {
        return await _workerRepository.Update(id, userId, specialization, name, Surname, hourlyRate, phoneNumber, email);
    }

    public async Task<int> DeleteWorker(int id)
    {
        return await _workerRepository.Delete(id);
    }
}
