using CRMSystem.Buisnes.Abstractions;
using CRMSystem.Core.DTOs;
using CRMSystem.DataAccess.Repositories;
using Microsoft.Extensions.Logging;

namespace CRMSystem.Buisnes.Services;

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
        _logger.LogInformation("Car status getting success");

        var carSatsus = await _carStatusRepository.Get();

        _logger.LogInformation("Car status getting success");

        return carSatsus;
    }
}
