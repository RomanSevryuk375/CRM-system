using CRMSystem.Buisnes.DTOs;
using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Repositories;

namespace CRMSystem.Buisnes.Services;

public class UsedPartService : IUsedPartService
{
    private readonly IUsedPartRepository _usedPartRepository;
    private readonly ISupplierRepository _supplierRepository;
    private readonly IWorkerRepository _workerRepository;
    private readonly IWorkRepository _workRepository;
    private readonly IOrderRepository _orderRepository;

    public UsedPartService(
        IUsedPartRepository usedPartRepository,
        ISupplierRepository supplierRepository,
        IWorkerRepository workerRepository,
        IWorkRepository workRepository,
        IOrderRepository orderRepository)
    {
        _usedPartRepository = usedPartRepository;
        _supplierRepository = supplierRepository;
        _workerRepository = workerRepository;
        _workRepository = workRepository;
        _orderRepository = orderRepository;
    }

    public async Task<List<UsedPart>> GetUsedPart()
    {
        return await _usedPartRepository.Get();
    }

    public async Task<List<UsedPartWithInfoDto>> GetUsedPartWithInfo()
    {
        var usedParts = await _usedPartRepository.Get();
        var suppliers = await _supplierRepository.Get();

        var response = (from u in usedParts
                        join s in suppliers on u.SupplierId equals s.Id
                        select new UsedPartWithInfoDto(
                            u.Id,
                            u.OrderId,
                            s.Name,
                            u.Name,
                            u.Article,
                            u.Quantity,
                            u.UnitPrice,
                            u.Sum)).ToList();

        return response;
    }

    public async Task<List<UsedPartWithInfoDto>> GetWorkerUsedPart(int userId)
    {
        //worker
        var worker = await _workerRepository.GetWorkerIdByUserId(userId);
        var workerId = worker.Select(w => w.Id).ToList();
        //work
        var works = await _workRepository.GetByWorkerId(workerId);
        var workIds = works.Select(w => w.OrderId).ToList();
        //order
        var orders = await _orderRepository.GetById(workIds);
        var orderIds = orders.Select(o => o.Id).ToList();
        //usedpart
        var usedParts = await _usedPartRepository.GetByOrderId(orderIds);
        var suppliers = await _supplierRepository.Get();

        var response = (from u in usedParts
                        join s in suppliers on u.SupplierId equals s.Id
                        select new UsedPartWithInfoDto(
                            u.Id,
                            u.OrderId,
                            s.Name,
                            u.Name,
                            u.Article,
                            u.Quantity,
                            u.UnitPrice,
                            u.Sum)).ToList();

        return response;
    }

    public async Task<int> CreateUsedPart(UsedPart usedPart)
    {
        return await _usedPartRepository.Create(usedPart);
    }

    public async Task<int> UpdateUsedPart(int id, int orderId, int supplierId, string name, string article, decimal quantity, decimal unitPrice, decimal sum)
    {
        return await _usedPartRepository.Update(id, orderId, supplierId, name, article, quantity, unitPrice, sum);
    }

    public async Task<int> DeleteUsedPart(int id)
    {
        return await _usedPartRepository.Delete(id);
    }
}
