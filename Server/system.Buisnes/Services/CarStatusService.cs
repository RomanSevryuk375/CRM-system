using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels;
using Microsoft.Extensions.Logging;

namespace CRMSystem.Business.Services;

public class CarStatusService : ICarStatusService
{
    private readonly ICarStatusRepository _carStatusRepository;
    private readonly ILogger<CarStatusService> _logger;

    public CarStatusService(
        ICarStatusRepository carStatusRepository,
        ILogger<CarStatusService> logger)
    {
        _carStatusRepository = carStatusRepository;
        _logger = logger;
    }

    public async Task<List<CarStatusItem>> GetCarStatuses()
    {
        _logger.LogInformation("Car status getting start");

        var carSatsus = await _carStatusRepository.Get();

        _logger.LogInformation("Car status getting success");

        return carSatsus;
    }
}
