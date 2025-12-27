using CRMSystem.Buisnes.Abstractions;
using CRMSystem.Core.DTOs.Supply;
using CRMSystem.Core.Exceptions;
using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Repositories;
using Microsoft.Extensions.Logging;

namespace CRMSystem.Buisnes.Services;

public class SupplyService : ISupplyService
{
    private readonly ISupplyRepository _supplyRepository;
    private readonly ISupplierRepository _supplierRepository;
    private readonly ILogger<SupplyService> _logger;

    public SupplyService(
        ISupplyRepository supplySetRepository,
        ISupplierRepository supplierRepository,
        ILogger<SupplyService> logger)
    {
        _supplyRepository = supplySetRepository;
        _supplierRepository = supplierRepository;
        _logger = logger;
    }

    public async Task<List<SupplyItem>> GetPagedSupplies(SupplyFilter filter)
    {
        _logger.LogInformation("Getting supplies start");

        var supplies = await _supplyRepository.GetPaged(filter);

        _logger.LogInformation("Getting supplies success");

        return supplies;
    }

    public async Task<int> GetCountSupplies(SupplyFilter filter)
    {
        _logger.LogInformation("Getting supplies count start");

        var count = await _supplyRepository.GetCount(filter);

        _logger.LogInformation("Getting supplies count success");

        return count;
    }

    public async Task<long> CreateSupply(Supply supply)
    {
        _logger.LogInformation("Creating supplies start");

        if (!await _supplierRepository.Exists(supply.SupplierId))
        {
            _logger.LogError("Supplier{supplierId} not found", supply.SupplierId);
            throw new NotFoundException($"Supplier {supply.SupplierId} not found");
        }

        var Id = await _supplyRepository.Create(supply);

        _logger.LogInformation("Creating supplies success");

        return Id;
    }

    public async Task<long> DeleteSupply(long id)
    {
        _logger.LogInformation("Deleting supplies start");

        var Id = await _supplyRepository.Delete(id);

        _logger.LogInformation("Deleting supplies success");

        return Id;
    }
}
