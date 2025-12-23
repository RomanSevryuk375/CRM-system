using CRMSystem.Buisnes.Abstractions;
using CRMSystem.Buisnes.Extensions;
using CRMSystem.Core.DTOs.Car;
using CRMSystem.Core.Models;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace CRMSystem.Buisnes.Cached;

public class CachedCarService : ICarService
{
    private readonly ICarService _decorated;
    private readonly IDistributedCache _distributed;
    private readonly ILogger<CachedCarService> _logger;

    public CachedCarService(
        ICarService decorated,
        IDistributedCache distributed,
        ILogger<CachedCarService> logger)
    {
        _decorated = decorated;
        _distributed = distributed;
        _logger = logger;
    }
    public async Task<long> CreateCar(Car car)
    {
        return await _decorated.CreateCar(car);
    }

    public async Task<long> DeleteCar(long id)
    {
        await _distributed.RemoveAsync($"car_{id}");

        _logger.LogInformation("Removing cache success");

        return await _decorated.DeleteCar(id);
    }

    public async Task<CarItem> GetCarById(long id)
    {
        var key = $"car_{id}";

        return await _distributed.GetOrCreateAsync(
            key,
            () => _decorated.GetCarById(id),
            TimeSpan.FromMinutes(2),
            _logger);
    }

    public async Task<int> GetCountCars(CarFilter filter)
    {
        return await _decorated.GetCountCars(filter);
    }

    public async Task<List<CarItem>> GetPagedCars(CarFilter filter)
    {
        return await _decorated.GetPagedCars(filter);
    }

    public async Task<long> UpdateCar(long id, CarUpdateModel model)
    {
        await _distributed.RemoveAsync($"car_{id}");

        _logger.LogInformation("Removing cache success");

        return await _decorated.UpdateCar(id, model);
    }
}
