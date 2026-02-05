using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels.SupplySet;
using CRMSystem.Core.Exceptions;
using CRMSystem.Core.Models;
using Microsoft.Extensions.Logging;
using Shared.Filters;

namespace CRMSystem.Business.Services;

public class SupplySetService(
    ISupplySetRepository supplySetRepository,
    ISupplyRepository supplyRepository,
    IPositionRepository positionRepository,
    ILogger<SupplySetService> logger) : ISupplySetService
{
    public async Task<List<SupplySetItem>> GetPagedSupplySets(SupplySetFilter filter, CancellationToken ct)
    {
        logger.LogInformation("Getting supply sets start");

        var supplySets = await supplySetRepository.GetPaged(filter, ct);

        logger.LogInformation("Getting supply sets success");

        return supplySets;
    }

    public async Task<int> GetCountSupplySets(SupplySetFilter filter, CancellationToken ct)
    {
        logger.LogInformation("Getting count supply sets start");

        var count = await supplySetRepository.GetCount(filter, ct);

        logger.LogInformation("Getting count supply sets success");

        return count;
    }

    public async Task<long> CreateSupplySet(SupplySet supplySet, CancellationToken ct)
    {
        logger.LogInformation("Creating supply sets start");

        if (!await supplyRepository.Exists(supplySet.SupplyId, ct))
        {
            logger.LogError("Supply{supplyId} not found", supplySet.SupplyId);
            throw new NotFoundException($"Supply {supplySet.SupplyId} not found");
        }

        if (!await positionRepository.Exists(supplySet.PositionId, ct))
        {
            logger.LogError("Position{positionId} not found", supplySet.PositionId);
            throw new NotFoundException($"Position {supplySet.PositionId} not found");
        }

        var Id = await supplySetRepository.Create(supplySet, ct);

        logger.LogInformation("Creating supply sets success");

        return Id;
    }

    public async Task<long> UpdateSupplySet(long id, SupplySetUpdateModel model, CancellationToken ct)
    {
        logger.LogInformation("Updating supply sets start");

        var Id = await supplySetRepository.Update(id, model, ct);

        logger.LogInformation("Updating supply sets success");

        return Id;
    }

    public async Task<long> DeleteSupplySet(long id, CancellationToken ct)
    {
        logger.LogInformation("Deleting supply sets start");

        var Id = await supplySetRepository.Delete(id, ct);

        logger.LogInformation("Deleting supply sets success");

        return Id;
    }
}
