using CRMSystem.Buisnes.DTOs;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Repositories;

namespace CRMSystem.Buisnes.Services;

public class RepairNoteService : IRepairNoteService
{
    private readonly IRepairNoteRepositry _repairNoteRepositry;
    private readonly ICarRepository _carRepository;
    private readonly IClientRepository _clientsRepository;
    private readonly IWorkerRepository _workerRepository;
    private readonly IWorkRepository _workRepository;
    private readonly IOrderRepository _orderRepository;

    public RepairNoteService(
        IRepairNoteRepositry repairNoteRepositry,
        ICarRepository carRepository,
        IClientRepository clientsRepository,
        IWorkerRepository workerRepository,
        IWorkRepository workRepository,
        IOrderRepository orderRepository)
    {
        _repairNoteRepositry = repairNoteRepositry;
        _carRepository = carRepository;
        _clientsRepository = clientsRepository;
        _workerRepository = workerRepository;
        _workRepository = workRepository;
        _orderRepository = orderRepository;
    }

    public async Task<List<RepairNote>> GetPagedRepairNote(int page, int limit)
    {
        return await _repairNoteRepositry.GetPaged(page, limit);
    }

    public async Task<int> GetCountRepairNote()
    {
        return await _repairNoteRepositry.GetCount();
    }

    public async Task<List<RepairNoteWithInfoDto>> GetPagedRepairNoteWithInfo(int page, int limit)
    {
        var repairNotes = await _repairNoteRepositry.GetPaged(page, limit);
        var cars = await _carRepository.Get();

        var response = (from r in repairNotes
                        join c in cars on r.CarId equals c.Id
                        select new RepairNoteWithInfoDto(
                            r.Id,
                            r.OrderId,
                            $"{c.Brand} {c.Model} ({c.StateNumber})",
                            r.WorkDate,
                            r.ServiceSum))
                            .ToList();

        return response;
    }

    public async Task<List<RepairNoteWithInfoDto>> GetPagedUserRepairNote(int userId, int page, int limit)
    {
        var client = await _clientsRepository.GetClientByUserId(userId);
        var clientId = client.Select(c => c.Id).FirstOrDefault();

        var cars = await _carRepository.GetByOwnerId(clientId);
        var carIds = cars.Select(c => c.Id).ToList();

        var repairNotes = await _repairNoteRepositry.GetPagedByCarId(carIds, page, limit);

        var response = (from r in repairNotes
                        join c in cars on r.CarId equals c.Id
                        select new RepairNoteWithInfoDto(
                            r.Id,
                            r.OrderId,
                            $"{c.Brand} {c.Model} ({c.StateNumber})",
                            r.WorkDate,
                            r.ServiceSum))
                            .ToList();

        return response;
    }

    public async Task<int> GetCountUserRepairNote(int userId)
    {
        var client = await _clientsRepository.GetClientByUserId(userId);
        var clientId = client.Select(c => c.Id).FirstOrDefault();

        var cars = await _carRepository.GetByOwnerId(clientId);
        var carIds = cars.Select(c => c.Id).ToList();

        return await _repairNoteRepositry.GetCountByCarId(carIds);
    }

    public async Task<List<RepairNoteWithInfoDto>> GetPagedWorkerRepairNote(int userId, int page, int limit)
    {
        var worker = await _workerRepository.GetWorkerByUserId(userId);
        var workerId = worker.Select(w => w.Id).ToList();

        var works = await _workRepository.GetByWorkerId(workerId);

        var cars = await _carRepository.Get();

        var orderId = works.Select(works => works.OrderId).ToList();

        var repairNotes = await _repairNoteRepositry.GetPagedByOrderId(orderId, page, limit);

        var response = (from r in repairNotes
                        join c in cars on r.CarId equals c.Id
                        select new RepairNoteWithInfoDto(
                            r.Id,
                            r.OrderId,
                            $"{c.Brand} {c.Model} ({c.StateNumber})",
                            r.WorkDate,
                            r.ServiceSum))
                            .ToList();

        return response;
    }

    public async Task<int> GetCountWorkerRepairNote(int userId)
    {
        var worker = await _workerRepository.GetWorkerByUserId(userId);
        var workerId = worker.Select(w => w.Id).ToList();

        var works = await _workRepository.GetByWorkerId(workerId);

        var cars = await _carRepository.Get();

        var orderId = works.Select(works => works.OrderId).ToList();

        return await _repairNoteRepositry.GetCountByOrderId(orderId);
    }
}
