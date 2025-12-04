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

    public async Task<List<UsedPart>> GetPagedUsedPart(int page, int limit)
    {
        return await _usedPartRepository.GetPaged(page, limit);
    }

    public async Task<int> GetCountUsedPart()
    {
        return await _usedPartRepository.GetCount();
    }

    public async Task<List<UsedPartWithInfoDto>> GetPagedUsedPartWithInfo(int page, int limit)
    {
        var usedParts = await _usedPartRepository.GetPaged(page, limit);
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

    public async Task<List<UsedPartWithInfoDto>> GetPagedWorkerUsedPart(int userId, int page, int limit)
    {

        var worker = await _workerRepository.GetWorkerByUserId(userId);
        var workerId = worker.Select(w => w.Id).ToList();

        var works = await _workRepository.GetByWorkerId(workerId);
        var workIds = works.Select(w => w.OrderId).ToList();

        var orders = await _orderRepository.GetById(workIds);
        var orderIds = orders.Select(o => o.Id).ToList();

        var usedParts = await _usedPartRepository.GetByPagedOrderId(orderIds, page, limit);
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

    public async Task<int> GetCountWorkerUsedPart(int userId)
    {
        var worker = await _workerRepository.GetWorkerByUserId(userId);
        var workerId = worker.Select(w => w.Id).ToList();

        var works = await _workRepository.GetByWorkerId(workerId);
        var workIds = works.Select(w => w.OrderId).ToList();

        var orders = await _orderRepository.GetById(workIds);
        var orderIds = orders.Select(o => o.Id).ToList();

        return await _usedPartRepository.GetCountByOrderId(orderIds);
    }

    public async Task<int> CreateUsedPart(UsedPart usedPart)
    {
        return await _usedPartRepository.Create(usedPart);
    }

    public async Task<int> UpdateUsedPart(int id, int? orderId, int? supplierId, string? name, string? article, decimal? quantity, decimal? unitPrice, decimal? sum)
    {
        return await _usedPartRepository.Update(id, orderId, supplierId, name, article, quantity, unitPrice, sum);
    }

    public async Task<int> DeleteUsedPart(int id)
    {
        return await _usedPartRepository.Delete(id);
    }
}
