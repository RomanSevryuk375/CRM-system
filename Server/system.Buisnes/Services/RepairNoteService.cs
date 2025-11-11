using CRMSystem.Buisnes.DTOs;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Repositories;

namespace CRMSystem.Buisnes.Services;

public class RepairNoteService : IRepairNoteService
{
    private readonly IRepairNoteRepositry _repairNoteRepositry;
    private readonly ICarRepository _carRepository;
    private readonly IClientsRepository _clientsRepository;
    private readonly IWorkerRepository _workerRepository;
    private readonly IWorkRepository _workRepository;
    private readonly IOrderRepository _orderRepository;

    public RepairNoteService(
        IRepairNoteRepositry repairNoteRepositry,
        ICarRepository carRepository,
        IClientsRepository clientsRepository,
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

    public async Task<List<RepairNote>> GetRepairNote()
    {
        return await _repairNoteRepositry.Get();
    }

    public async Task<List<RepairNoteWithInfoDto>> GetRepairNoteWithInfo()
    {
        var repairNotes = await _repairNoteRepositry.Get();
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

    public async Task<List<RepairNoteWithInfoDto>> GetUserRepairNote(int userId)
    {
        var client = await _clientsRepository.GetClientByUserId(userId);
        var clientId = client.Select(c => c.Id).FirstOrDefault();

        var cars = await _carRepository.GetByOwnerId(clientId);
        var carIds = cars.Select(c => c.Id).ToList();

        var repairNotes = await _repairNoteRepositry.GetByCarId(carIds);

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

    public async Task<List<RepairNoteWithInfoDto>> GetWorkerRepairNote(int userId)
    {
        var worker = await _workerRepository.GetWorkerIdByUserId(userId);
        var workerId = worker.Select(w => w.Id).ToList();

        var works = await _workRepository.GetByWorkerId(workerId);

        var cars = await _carRepository.Get();

        var orderId = works.Select(works => works.OrderId).ToList();

        var repairNotes = await _repairNoteRepositry.GetByOrderId(orderId);

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
}
