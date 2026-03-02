using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels.Supply;
using CRMSystem.Core.Exceptions;
using CRMSystem.Core.Models;
using Microsoft.Extensions.Logging;
using Shared.Filters;

namespace CRMSystem.Business.Services;

public class SupplyService(
    ISupplyRepository supplyRepository,
    ISupplierRepository supplierRepository,
    ILogger<SupplyService> logger) : ISupplyService
{
    public async Task<SupplyItem> GetSupplyById(long id, CancellationToken ct)
    {
        return await supplyRepository.GetById(id, ct)
               ?? throw new NotFoundException($"Supply {id} not found");
    }
    
    public async Task<List<SupplyItem>> GetPagedSupplies(SupplyFilter filter, CancellationToken ct)
    {
        logger.LogInformation("Getting supplies start");

        var supplies = await supplyRepository.GetPaged(filter, ct);

        logger.LogInformation("Getting supplies success");

        return supplies;
    }

    public async Task<int> GetCountSupplies(SupplyFilter filter, CancellationToken ct)
    {
        logger.LogInformation("Getting supplies count start");

        var count = await supplyRepository.GetCount(filter, ct);

        logger.LogInformation("Getting supplies count success");

        return count;
    }

    public async Task<long> CreateSupply(SupplyCreateModel createModel, CancellationToken ct)
    {
        logger.LogInformation("Creating supplies start");

        if (!await supplierRepository.Exists(createModel.SupplierId, ct))
        {
            logger.LogError("Supplier{supplierId} not found", createModel.SupplierId);
            throw new NotFoundException($"Supplier {createModel.SupplierId} not found");
        }

        var (supply, errors) = Supply.Create(
            0,
            createModel.SupplierId,
            createModel.Date);

        if (errors is not null && errors.Any())
        {
            throw new ValidationException(string.Join(", ", errors));
        }
        
        var id = await supplyRepository.Create(supply!, ct);

        logger.LogInformation("Creating supplies success");

        return id;
    }

    public async Task<long> DeleteSupply(long id, CancellationToken ct)
    {
        logger.LogInformation("Deleting supplies start");

        var supplyId = await supplyRepository.Delete(id, ct);

        logger.LogInformation("Deleting supplies success");

        return supplyId;
    }
}
