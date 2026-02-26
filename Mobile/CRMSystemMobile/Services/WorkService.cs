using Shared.Contracts.Work;
using Shared.Filters;
using System.Diagnostics;
using System.Net.Http.Json;
using CRMSystemMobile.Extensions;

namespace CRMSystemMobile.Services;

public class WorkService(HttpClient httpClient)
{
    public async Task<(List<WorkResponse>?, int TotalCount)> GetWorks(WorkFilter filter)
    {
        return await httpClient.GetPagedAsync<WorkResponse>("api/v1/works", filter);
    }

    public async Task<string?> CreateWork(WorkRequest request)
    {
        try
        {
            var response = await httpClient.PostAsJsonAsync("api/v1/works", request);

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
            return null;
        }
    }

    public async Task<string?> DeleteWork(long id)
    {
        try
        {
            var response = await httpClient.DeleteAsync($"api/v1/works/{id}");

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
            return null;
        }
    }
}