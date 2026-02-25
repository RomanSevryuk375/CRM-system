using Shared.Contracts.Schedule;
using System.Diagnostics;
using System.Net.Http.Json;
using CRMSystemMobile.Extensions;
using Shared.Filters;

namespace CRMSystemMobile.Services;

public class ScheduleService(HttpClient httpClient)
{
    public async Task<(List<ScheduleResponse>?, int TotalCount)> GetMySchedules(ScheduleFilter filter)
    {
        return await httpClient.GetPagedAsync<ScheduleResponse>("api/v1/schedules", filter);
    }
}