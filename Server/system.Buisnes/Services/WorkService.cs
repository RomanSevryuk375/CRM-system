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
    private readonly IOrderRepository _orderRepository;

    public WorkService(
        IWorkRepository workRepository,
        IStatusRepository statusRepository,
        IWorkTypeRepository workTypeRepository,
        IWorkerRepository workerRepository,
        IOrderRepository orderRepository)
    {
        _workRepository = workRepository;
        _statusRepository = statusRepository;
        _workTypeRepository = workTypeRepository;
        _workerRepository = workerRepository;
        _orderRepository = orderRepository;
    }

    public async Task<List<Work>> GetPagedWork(int page, int limit)
    {
        return await _workRepository.GetPaged(page, limit);
    }

    public async Task<int> GetCountWork()
    {
        return await _workRepository.GetCount();
    }

    public async Task<List<Work>> GetPagedByWorkerId(List<int> workerId, int page, int limit)
    {
        return await _workRepository.GetPagedByWorkerId(workerId, page, limit);
    }

    public async Task<List<WorkWithInfoDto>> GetPagedInWorkWorks(int userId, int page, int limit)
    {
        var worker = await _workerRepository.GetWorkerByUserId(userId);
        var workerIds = worker.Select(w => w.Id).ToList();

        var allWorks = await _workRepository.GetPagedByWorkerId(workerIds, page, limit);
        var inWork = allWorks.Where(i => i.StatusId == 4).ToList();
        var statuses = await _statusRepository.Get();
        var workTypes = await _workTypeRepository.Get();
        var workers = await _workerRepository.Get();

        var response = (from w in inWork
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

    public async Task<int> GetCoutInWorkWorks(int userId)
    {
        var worker = await _workerRepository.GetWorkerByUserId(userId);
        var workerIds = worker.Select(w => w.Id).ToList();

        return await _workRepository.GetCountByWorkerId(workerIds);
    }

    public async Task<List<WorkWithInfoDto>> GetPagedWorkWithInfo(int page, int limit)
    {
        var works = await _workRepository.GetPaged(page, limit);
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

    public async Task<List<WorkWithInfoDto>> GetWorkByCarId(int carId)
    {
        var order = await _orderRepository.GetByCarId(carId);
        var orderId = order.Select(w => w.Id).ToList();
        var works = await _workRepository.GetByOrderId(orderId);
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

    public async Task<int> UpdateWork(int id, int? orderId, int? jobId, int? workerId, decimal? timeSpent, int? statusId)
    {
        return await _workRepository.Update(id, orderId, jobId, workerId, timeSpent, statusId);
    }

    public async Task<int> DeleteWork(int id)
    {
        return await _workRepository.Delete(id);
    }
}
