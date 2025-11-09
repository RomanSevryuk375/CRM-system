using CRMSystem.Buisnes.DTOs;
using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Repositories;

namespace CRMSystem.Buisnes.Services;

public class WorkService : IWorkService
{
    private readonly IWorkRepository _workRepository;
    private readonly IStatusRepository _statusRepository;
    private readonly IWorkTypeRepository _workTypeRepository;
    private readonly IWorkerRepository _workerRepository;

    public WorkService(
        IWorkRepository workRepository,
        IStatusRepository statusRepository,
        IWorkTypeRepository workTypeRepository,
        IWorkerRepository workerRepository)
    {
        _workRepository = workRepository;
        _statusRepository = statusRepository;
        _workTypeRepository = workTypeRepository;
        _workerRepository = workerRepository;
    }

    public async Task<List<Work>> GetWork()
    {
        return await _workRepository.Get();
    }

    public async Task<List<Work>> GetByWorkerId(int workerId)
    {
        return await _workRepository.GetByWorkerId(workerId);
    }

    public async Task<List<WorkWithInfoDto>> GetWorkWithInfo()
    {
        var works = await _workRepository.Get();
        var statuses = await _statusRepository.Get();
        var workTypes = await _workTypeRepository.Get();
        var workers = await _workerRepository.Get();

        var response = (from w in works
                        join s in statuses on w.StatusId equals s.Id
                        join wt in workTypes on w.JobId equals wt.Id
                        join wo in workers on w.WorkerId equals wo.Id
                        select new WorkWithInfoDto(
                            w.Id,
                            w.OrderId,
                            $"{wt.Title}",
                            $"{wo.Name} {wo.Surname} ({wo.PhoneNumber})",
                            w.TimeSpent,
                            s.Name))
                            .ToList();

        return response;
    }

    public async Task<int> CreateWork(Work work)
    {
        return await _workRepository.Create(work);
    }

    public async Task<int> UpdateWork(int id, int orderId, int jobId, int workerId, decimal timeSpent, int statusId)
    {
        return await _workRepository.Update(id, orderId, jobId, workerId, timeSpent, statusId);
    }

    public async Task<int> DeleteWork(int id)
    {
        return await _workRepository.Delete(id);
    }
}
