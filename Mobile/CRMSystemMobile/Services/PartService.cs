using Shared.Contracts.Part;
using Shared.Filters;
using System.Diagnostics;
using System.Net.Http.Json;
using CRMSystemMobile.Extensions;

namespace CRMSystemMobile.Services;

public class PartService(HttpClient httpClient)
{
    public async Task<(List<PartResponse>?, int TotalCount)> GetParts(PartFilter filter)
    {
        return await httpClient.GetPagedAsync<PartResponse>("api/v1/parts", filter);
    }

    public async Task<string?> CreatePart(PartRequest request)
    {
        try
        {
            var response = await httpClient.PostAsJsonAsync("api/v1/parts", request);

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

    public async Task<string?> DeletePart(long id)
    {
        try
        {
            var response = await httpClient.DeleteAsync($"api/v1/parts/{id}");

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
}