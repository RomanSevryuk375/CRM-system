using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels;
using Microsoft.Extensions.Logging;

namespace CRMSystem.Business.Services;

public class CarStatusService(
    ICarStatusRepository carStatusRepository,
    ILogger<CarStatusService> logger) : ICarStatusService
{
    public async Task<List<CarStatusItem>> GetCarStatuses(CancellationToken ct)
    {
        logger.LogInformation("Car status getting start");

        var carSatsus = await carStatusRepository.Get(ct);

        logger.LogInformation("Car status getting success");

        return carSatsus;
    }
}
