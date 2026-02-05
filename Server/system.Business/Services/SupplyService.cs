using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels.Supply;
using CRMSystem.Core.Exceptions;
using CRMSystem.Core.Models;
using Microsoft.Extensions.Logging;
using Shared.Filters;

namespace CRMSystem.Business.Services;

public class SupplyService(
    ISupplyRepository supplySetRepository,
    ISupplierRepository supplierRepository,
    ILogger<SupplyService> logger) : ISupplyService
{
    public async Task<List<SupplyItem>> GetPagedSupplies(SupplyFilter filter, CancellationToken ct)
    {
        logger.LogInformation("Getting supplies start");

        var supplies = await supplySetRepository.GetPaged(filter, ct);

        logger.LogInformation("Getting supplies success");

        return supplies;
    }

    public async Task<int> GetCountSupplies(SupplyFilter filter, CancellationToken ct)
    {
        logger.LogInformation("Getting supplies count start");

        var count = await supplySetRepository.GetCount(filter, ct);

        logger.LogInformation("Getting supplies count success");

        return count;
    }

    public async Task<long> CreateSupply(Supply supply, CancellationToken ct)
    {
        logger.LogInformation("Creating supplies start");

        if (!await supplierRepository.Exists(supply.SupplierId, ct))
        {
            logger.LogError("Supplier{supplierId} not found", supply.SupplierId);
            throw new NotFoundException($"Supplier {supply.SupplierId} not found");
        }

        var Id = await supplySetRepository.Create(supply, ct);

        logger.LogInformation("Creating supplies success");

        return Id;
    }

    public async Task<long> DeleteSupply(long id, CancellationToken ct)
    {
        logger.LogInformation("Deleting supplies start");

        var Id = await supplySetRepository.Delete(id, ct);

        logger.LogInformation("Deleting supplies success");

        return Id;
    }
}
