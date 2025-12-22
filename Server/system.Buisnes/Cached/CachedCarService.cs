using CRMSystem.Buisnes.Abstractions;
using CRMSystem.Core.DTOs.Car;
using CRMSystem.Core.Exceptions;
using CRMSystem.Core.Models;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

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
        string key = $"car_{id}";

        var cachedCar = await _distributed.GetStringAsync(key);

        CarItem? car;
        if (string.IsNullOrEmpty(cachedCar))
        {
            _logger.LogInformation("Returning car from Db");

            car = await _decorated.GetCarById(id);


            if (car is null)
                return car!;

            await _distributed.SetStringAsync(
                key,
                JsonConvert.SerializeObject(car),
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(2)
                });

            _logger.LogInformation("Caching car sucess");

            return car;
        }

        _logger.LogInformation("Returning car from cache");

        car = JsonConvert.DeserializeObject<CarItem>(cachedCar);

        return car!;
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
