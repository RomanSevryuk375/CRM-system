using CRMSystem.Buisnes.DTOs;
using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Repositories;

namespace CRMSystem.Buisnes.Services;

public class WorkPropossalService : IWorkPropossalService
{
    private readonly IWorkPropossalRepository _workPropossal;
    private readonly IWorkTypeRepository _workTypeRepository;
    private readonly IStatusRepository _statusRepository;
    private readonly IWorkerRepository _workerRepository;

    public WorkPropossalService(
        IWorkPropossalRepository workPropossal,
        IWorkTypeRepository workTypeRepository,
        IStatusRepository statusRepository,
        IWorkerRepository workerRepository)
    {
        _workPropossal = workPropossal;
        _workTypeRepository = workTypeRepository;
        _statusRepository = statusRepository;
        _workerRepository = workerRepository;
    }

    public async Task<List<WorkProposal>> GetWorkProposal()
    {
        return await _workPropossal.Get();
    }

    public async Task<List<WorkProposalWithInfoDto>> GetWorkProposalWithInfo()
    {
        var workProposals = await _workPropossal.Get();
        var workTypes = await _workTypeRepository.Get();
        var workers = await _workerRepository.Get();
        var statuses = await _statusRepository.Get();

        var response = (from wp in workProposals
                        join wt in workTypes on wp.WorkId equals wt.Id
                        join s in statuses on wp.StatusId equals s.Id
                        join w in workers on wp.ByWorker equals w.Id
                        select new WorkProposalWithInfoDto(
                            wp.Id,
                            wp.OrderId,
                            wt.Title,
                            $"{w.Name} {w.Surname}",
                            s.Name,
                            s.Name,
                            wp.Date)).ToList();

        return response;
    }

    public async Task<int> CreateWorkProposal(WorkProposal workProposal)
    {
        return await _workPropossal.Create(workProposal);
    }

    public async Task<int> UpdateWorkProposal(int id, int orderId, int workId, int byWorker, int statusId, int decisionStatusId, DateTime date)
    {
        return await _workPropossal.Update(id, orderId, workId, byWorker, statusId, decisionStatusId, date);
    }

    public async Task<int> DeleteWorkProposal(int id)
    {
        return await _workPropossal.Delete(id);
    }
}
