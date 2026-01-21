using CRMSystem.Business.Abstractions;
using CRMSystem.Business.Extensions;
using CRMSystem.Core.ProjectionModels.Car;
using CRMSystem.Core.Models;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Shared.Filters;

namespace CRMSystem.Business.Cached;

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
    public async Task<long> CreateCar(Car car, CancellationToken ct)
    {
        return await _decorated.CreateCar(car, ct);
    }

    public async Task<long> DeleteCar(long id, CancellationToken ct)
    {
        await _distributed.RemoveAsync($"car_{id}", ct);

        _logger.LogInformation("Removing cache success");

        return await _decorated.DeleteCar(id, ct);
    }

    public async Task<CarItem> GetCarById(long id, CancellationToken ct)
    {
        var key = $"car_{id}";

        return await _distributed.GetOrCreateAsync(
            key,
            () => _decorated.GetCarById(id, ct),
            TimeSpan.FromMinutes(2),
            _logger, ct);
    }

    public async Task<int> GetCountCars(CarFilter filter, CancellationToken ct)
    {
        return await _decorated.GetCountCars(filter, ct);
    }

    public async Task<List<CarItem>> GetPagedCars(CarFilter filter, CancellationToken ct)
    {
        return await _decorated.GetPagedCars(filter, ct);
    }

    public async Task<long> UpdateCar(long id, CarUpdateModel model, CancellationToken ct)
    {
        await _distributed.RemoveAsync($"car_{id}", ct);

        _logger.LogInformation("Removing cache success");

        return await _decorated.UpdateCar(id, model, ct);
    }
}
