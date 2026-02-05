using CRMSystem.Business.Abstractions;
using CRMSystem.Business.Extensions;
using CRMSystem.Core.ProjectionModels.Car;
using CRMSystem.Core.Models;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Shared.Filters;

namespace CRMSystem.Business.Cached;

public class CachedCarService(
    ICarService decorated,
    IDistributedCache distributed,
    ILogger<CachedCarService> logger) : ICarService
{
    public async Task<long> CreateCar(Car car, CancellationToken ct)
    {
        return await decorated.CreateCar(car, ct);
    }

    public async Task<long> DeleteCar(long id, CancellationToken ct)
    {
        await distributed.RemoveAsync($"car_{id}", ct);

        logger.LogInformation("Removing cache success");

        return await decorated.DeleteCar(id, ct);
    }

    public async Task<CarItem> GetCarById(long id, CancellationToken ct)
    {
        var key = $"car_{id}";

        return await distributed.GetOrCreateAsync(
            key,
            () => decorated.GetCarById(id, ct),
            TimeSpan.FromMinutes(2),
            logger, ct);
    }

    public async Task<int> GetCountCars(CarFilter filter, CancellationToken ct)
    {
        return await decorated.GetCountCars(filter, ct);
    }

    public async Task<List<CarItem>> GetPagedCars(CarFilter filter, CancellationToken ct)
    {
        return await decorated.GetPagedCars(filter, ct);
    }

    public async Task<long> UpdateCar(long id, CarUpdateModel model, CancellationToken ct)
    {
        await distributed.RemoveAsync($"car_{id}", ct);

        logger.LogInformation("Removing cache success");

        return await decorated.UpdateCar(id, model, ct);
    }
}
