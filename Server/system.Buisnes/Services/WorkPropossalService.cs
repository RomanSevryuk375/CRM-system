using CRMSystem.Buisnes.DTOs;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Repositories;

namespace CRMSystem.Buisnes.Services;

public class WorkPropossalService : IWorkPropossalService
{
    private readonly IWorkPropossalRepository _workPropossal;
    private readonly IWorkTypeRepository _workTypeRepository;
    private readonly IStatusRepository _statusRepository;
    private readonly IWorkerRepository _workerRepository;
    private readonly IClientsRepository _clientsRepository;
    private readonly ICarRepository _carRepository;
    private readonly IOrderRepository _orderRepository;

    public WorkPropossalService(
        IWorkPropossalRepository workPropossal,
        IWorkTypeRepository workTypeRepository,
        IStatusRepository statusRepository,
        IWorkerRepository workerRepository,
        IClientsRepository clientsRepository,
        ICarRepository carRepository,
        IOrderRepository orderRepository)
    {
        _workPropossal = workPropossal;
        _workTypeRepository = workTypeRepository;
        _statusRepository = statusRepository;
        _workerRepository = workerRepository;
        _clientsRepository = clientsRepository;
        _carRepository = carRepository;
        _orderRepository = orderRepository;
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

    public async Task<List<WorkProposalWithInfoDto>> GetProposalForClient(int userId)
    {
        //client
        var client = await _clientsRepository.GetClientByUserId(userId);
        var clientId = client.Select(c => c.Id).FirstOrDefault();
        //car
        var cars = await _carRepository.GetByOwnerId(clientId);
        var carIds = cars.Select(c => c.Id).ToList();
        //Order
        var orders = await _orderRepository.GetByCarId(carIds);
        var orderIds = orders.Select(c => c.Id).ToList();
        //Proposal
        var workProposals = await _workPropossal.GetByOrderId(orderIds);
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

    public async Task<int> AcceptProposal(int id)
    {
        return await _workPropossal.AcceptProposal(id);
    }

    public async Task<int> RejectProposal(int id)
    {
        return await _workPropossal.RejectProposal(id);
    }
}
