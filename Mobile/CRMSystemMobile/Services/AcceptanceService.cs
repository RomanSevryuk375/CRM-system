using CRMSystemMobile.Extensions;
using Shared.Contracts.Acceptance;
using Shared.Filters;
using System.Net.Http.Json;

namespace CRMSystemMobile.Services;

public class AcceptanceService(HttpClient httpClient)
{
    public async Task<AcceptanceResponse?> GetAcceptanceByOrder(long orderId)
    {
        var filter = new AcceptanceFilter(
            AcceptanceIds: [],
            WorkerIds: [],
            OrderIds: [orderId],
            SortBy: null,
            Page: 1,
            Limit: 1,
            IsDescending: true
        );

        var (items, _) = await httpClient.GetPagedAsync<AcceptanceResponse>("api/v1/acceptances", filter);
        return items?.FirstOrDefault();
    }

    public async Task<(long? Id, string? Error)> CreateOrUpdateAcceptance(AcceptanceRequest request, long? existingId)
    {
        try
        {
            HttpResponseMessage response;
            if (existingId.HasValue)
            {
                response = await httpClient.PutAsJsonAsync($"api/v1/acceptances/{existingId}", request);
            }
            else
            {
                response = await httpClient.PostAsJsonAsync("api/v1/acceptances", request);
            }

            if (response.IsSuccessStatusCode)
            {
                var idString = await response.Content.ReadAsStringAsync();
                long.TryParse(idString, out var newId);
                return (existingId ?? newId, null);
            }

            var error = await response.Content.ReadAsStringAsync();
            return (null, error);
        }
        catch (Exception ex)
        {
            return (null, ex.Message);
        }
    }

    public async Task<List<AcceptanceResponse>?> GetAcceptancesByOrderIds(IEnumerable<long> orderIds)
    {
        var filter = new AcceptanceFilter(
            AcceptanceIds: [],
            WorkerIds: [],
            OrderIds: orderIds,
            SortBy: null,
            Page: 1,
            Limit: 100,
            IsDescending: true
        );

        var (items, _) = await httpClient.GetPagedAsync<AcceptanceResponse>("api/v1/acceptances", filter);
        return items;
    }

    public async Task<string?> SignAcceptanceByClient(long acceptanceId)
    {
        try
        {
            var request = new AcceptanceUpdateRequest { ClientSign = true };

            var response = await httpClient.PutAsJsonAsync($"api/v1/acceptances/{acceptanceId}", request);

            if (response.IsSuccessStatusCode) return null;

            return await response.Content.ReadAsStringAsync();
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }
}