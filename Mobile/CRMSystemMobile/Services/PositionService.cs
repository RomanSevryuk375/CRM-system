using Shared.Contracts.Position;
using Shared.Filters;
using System.Diagnostics;
using System.Net.Http.Json;
using CRMSystemMobile.Extensions;

namespace CRMSystemMobile.Services;

public class PositionService(HttpClient httpClient)
{
    public async Task<(List<PositionResponse>?, int TotalCount)> GetPositions(PositionFilter filter)
    {
        return await httpClient.GetPagedAsync<PositionResponse>("api/v1/positions", filter);
    }
}