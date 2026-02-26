using Shared.Contracts.PartSet;
using Shared.Filters;
using System.Diagnostics;
using System.Net.Http.Json;
using CRMSystemMobile.Extensions;

namespace CRMSystemMobile.Services;

public class PartSetService(HttpClient httpClient)
{
    public async Task<(List<PartSetResponse>?, int TotalCount)> GetPartSets(PartSetFilter filter)
    {
         return await httpClient.GetPagedAsync<PartSetResponse>("api/v1/part-sets", filter);
    }

    public async Task<List<PartSetResponse>?> GetPartsByOrder(long orderId)
    {
        try
        {
            var response = await httpClient.GetAsync($"api/v1/part-sets/orders/{orderId}");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var items = await response.Content.ReadFromJsonAsync<List<PartSetResponse>>();
            return items ?? [];
        }
        catch
        {
            return null;
        }
    }

    public async Task<string?> AddToSet(PartSetRequest request)
    {
        try
        {
            var response = await httpClient.PostAsJsonAsync("api/v1/part-sets", request);

            if (response.IsSuccessStatusCode)
            {
                return null;
            }

            var errorContent = await response.Content.ReadAsStringAsync();

            if (string.IsNullOrWhiteSpace(errorContent))
            {
                return $"Server error: {response.StatusCode}";
            }

            return errorContent.Trim('"');
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.ToString());
            return $"Connection error: {ex.Message}";
        }
    }

    public async Task<string?> DeletePartSet(long id)
    {
        try
        {
            var response = await httpClient.DeleteAsync($"api/v1/part-sets/{id}");

            if (response.IsSuccessStatusCode)
            {
                return null;
            }

            var errorContent = await response.Content.ReadAsStringAsync();

            return string.IsNullOrWhiteSpace(errorContent)
                ? $"Server error: {response.StatusCode}"
                : errorContent.Trim('"');
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.ToString());
            return $"Connection error: {ex.Message}";
        }
    }
}