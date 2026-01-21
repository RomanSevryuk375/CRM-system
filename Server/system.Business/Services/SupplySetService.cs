using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels.SupplySet;
using CRMSystem.Core.Exceptions;
using CRMSystem.Core.Models;
using Microsoft.Extensions.Logging;
using Shared.Filters;

namespace CRMSystem.Business.Services;

public class SupplySetService : ISupplySetService
{
    private readonly ISupplySetRepository _supplySetRepository;
    private readonly ISupplyRepository _supplyRepository;
    private readonly IPositionRepository _positionRepository;
    private readonly ILogger<SupplySetService> _logger;

    public SupplySetService(
        ISupplySetRepository supplySetRepository,
        ISupplyRepository supplyRepository,
        IPositionRepository positionRepository,
        ILogger<SupplySetService> logger)
    {
        _supplySetRepository = supplySetRepository;
        _supplyRepository = supplyRepository;
        _positionRepository = positionRepository;
        _logger = logger;
    }

    public async Task<List<SupplySetItem>> GetPagedSupplySets(SupplySetFilter filter, CancellationToken ct)
    {
        _logger.LogInformation("Getting supply sets start");

        var supplySets = await _supplySetRepository.GetPaged(filter, ct);

        _logger.LogInformation("Getting supply sets success");

        return supplySets;
    }

    public async Task<int> GetCountSupplySets(SupplySetFilter filter, CancellationToken ct)
    {
        _logger.LogInformation("Getting count supply sets start");

        var count = await _supplySetRepository.GetCount(filter, ct);

        _logger.LogInformation("Getting count supply sets success");

        return count;
    }

    public async Task<long> CreateSupplySet(SupplySet supplySet, CancellationToken ct)
    {
        _logger.LogInformation("Creating supply sets start");

        if (!await _supplyRepository.Exists(supplySet.SupplyId, ct))
        {
            _logger.LogError("Supply{supplyId} not found", supplySet.SupplyId);
            throw new NotFoundException($"Supply {supplySet.SupplyId} not found");
        }

        if (!await _positionRepository.Exists(supplySet.PositionId, ct))
        {
            _logger.LogError("Position{positionId} not found", supplySet.PositionId);
            throw new NotFoundException($"Position {supplySet.PositionId} not found");
        }

        var Id = await _supplySetRepository.Create(supplySet, ct);

        _logger.LogInformation("Creating supply sets success");

        return Id;
    }

    public async Task<long> UpdateSupplySet(long id, SupplySetUpdateModel model, CancellationToken ct)
    {
        _logger.LogInformation("Updating supply sets start");

        var Id = await _supplySetRepository.Update(id, model, ct);

        _logger.LogInformation("Updating supply sets success");

        return Id;
    }

    public async Task<long> DeleteSupplySet(long id, CancellationToken ct)
    {
        _logger.LogInformation("Deleting supply sets start");

        var Id = await _supplySetRepository.Delete(id, ct);

        _logger.LogInformation("Deleting supply sets success");

        return Id;
    }
}
