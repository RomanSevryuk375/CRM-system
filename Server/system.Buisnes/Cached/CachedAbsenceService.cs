using CRMSystem.Buisnes.Abstractions;
using CRMSystem.Buisnes.Services;
using CRMSystem.Core.DTOs.Absence;
using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Entites;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace CRMSystem.Buisnes.Caching;

public class CachedAbsenceService : IAbsenceService
{
    private readonly AbsenceService _decorated;
    private readonly IDistributedCache _distributedCache;

    public CachedAbsenceService(AbsenceService service, IDistributedCache distributedCache)
    {
        _decorated = service;
        _distributedCache = distributedCache;
    }

    private IEnumerable<int>? ApplyFilter(IEnumerable<int>? enumerable, AbsenceFilter filter)
    {
        if (filter.workerIds != null && filter.workerIds.Any())
            enumerable = filter.workerIds; 

        return enumerable;
    }

    public async Task<int> CreateAbsence(Absence absence)
    {
        return await _decorated.CreateAbsence(absence);
    }

    public async Task<int> DeleteAbsence(int id)
    {
        return await _decorated.DeleteAbsence(id);
    }

    public async Task<int> GetCountAbsence(AbsenceFilter filter)
    {
        return await _decorated.GetCountAbsence(filter);
    }

    public async Task<List<AbsenceItem>> GetPagedAbsence(AbsenceFilter filter)
    {
        IEnumerable<int>? keyValue = null;
        keyValue = ApplyFilter(keyValue, filter);
        var key = $"Absence-{keyValue}";

        if (key is not null)
        { 
            string? cachedAbsence = await _distributedCache
                .GetStringAsync(key);

            List<AbsenceItem>? absence;

            if (string.IsNullOrEmpty(cachedAbsence))
            {
                absence = await _decorated.GetPagedAbsence(filter);

                if (absence is null)
                    return absence;

                await _distributedCache.SetStringAsync(
                    key,
                    JsonConvert.SerializeObject(absence));

                return absence;
            }

            absence = JsonConvert.DeserializeObject<List<AbsenceItem>>(cachedAbsence);

            return absence;
        }

        return await _decorated.GetPagedAbsence(filter);
    }

    public async Task<int> UpdateAbsence(AbsenceUpdateModel model)
    {
        return await _decorated.UpdateAbsence(model);
    }
}
