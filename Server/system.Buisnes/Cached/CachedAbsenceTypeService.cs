using CRMSystem.Buisnes.Abstractions;
using CRMSystem.Buisnes.Services;
using CRMSystem.Core.DTOs;
using CRMSystem.Core.Models;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CRMSystem.Buisnes.Cached;

public class CachedAbsenceTypeService : IAbsenceTypeService
{
    private readonly IAbsenceTypeService _decorated;
    private readonly IDistributedCache _distributed;
    private readonly ILogger<AbsenceTypeService> _logger;

    private const string CACHE_KEY = "dict_absence_types";

    public CachedAbsenceTypeService(
        IAbsenceTypeService decorated,
        IDistributedCache distributed,
        ILogger<AbsenceTypeService> logger)
    {
        _decorated = decorated;
        _distributed = distributed;
        _logger = logger;
    }
    public async Task<int> CretaeAbsenceType(AbsenceType absenceType)
    {
        var Id = await _decorated.CretaeAbsenceType(absenceType);

        await _distributed.RemoveAsync(CACHE_KEY);

        _logger.LogInformation("Removing cache success");

        return Id;
    }

    public async Task<int> DeleteAbsenceType(int id)
    {
        var Id = await _decorated.DeleteAbsenceType(id);

        await _distributed.RemoveAsync(CACHE_KEY);

        _logger.LogInformation("Removing cache success");

        return Id;
    }

    public async Task<List<AbsenceTypeItem>> GetAllAbsenceType()
    {
        var cacedTypes = await _distributed.
            GetStringAsync(CACHE_KEY);

        List < AbsenceTypeItem >? types;

        if (string.IsNullOrEmpty(cacedTypes))
        {
            _logger.LogInformation("Returning AbsenceTypes from Db");

            types = await _decorated.GetAllAbsenceType();

            if (types is null)
                return new List<AbsenceTypeItem>();

            await _distributed.SetStringAsync
                (CACHE_KEY, 
                JsonConvert.SerializeObject(types),
                new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(24) });

            _logger.LogInformation("Caching AbsenceTypes sucess");

            return types;
        }

        _logger.LogInformation("Returning AbsenceTypes from cache");

        types = JsonConvert.DeserializeObject<List<AbsenceTypeItem>>(cacedTypes);
        if (types is null)
            return new List<AbsenceTypeItem>();

        return types;
    }

    public async Task<int> UpdateAbsenceType(int id, string name)
    {
        var Id = await _decorated.UpdateAbsenceType(id, name);

        await _distributed.RemoveAsync(CACHE_KEY);

        _logger.LogInformation("Removing cache success");

        return Id;
    }
}
