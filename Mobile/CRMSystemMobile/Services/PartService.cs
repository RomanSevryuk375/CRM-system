using Shared.Contracts.Part;
using Shared.Filters;
using System.Diagnostics;
using System.Net.Http.Json;

namespace CRMSystemMobile.Services;

public class PartService(HttpClient httpClient)
{
    public async Task<(List<PartResponse>?, int TotalCount)> GetParts(PartFilter filter)
    {
        try
        {
            var query = $"Page={filter.Page}&Limit={filter.Limit}&IsDescending={filter.IsDescending}";

            if (!string.IsNullOrWhiteSpace(filter.SortBy))
            {
                query += $"&SortBy={filter.SortBy}";
            }

            if (filter.CategoryIds?.Any() == true)
            {
                foreach(var id in filter.CategoryIds)
                {
                    query += $"&CategoryIds={id}";
                }
            }

            string url = $"api/v1/parts?{query}";

            var response = await httpClient.GetAsync(url);

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                SecureStorage.Default.Remove("jwt_token");
                await Shell.Current.GoToAsync("//LoginPage");
                return (null, 0);
            }

            response.EnsureSuccessStatusCode();

            int totalCount = 0;
            if (response.Headers.TryGetValues("x-total-count", out var values))
            {
                int.TryParse(values.FirstOrDefault(), out totalCount);
            }

            var items = await response.Content.ReadFromJsonAsync<List<PartResponse>>();

            return(items, totalCount);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.ToString());
            return (null, 0);
        }
    }

    public async Task<string?> CreatePart (PartRequest request)
    {
        try
        {
            var response = await httpClient.PostAsJsonAsync("api/v1/parts", request);
            
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

        } catch (Exception ex)
        {
            Debug.WriteLine(ex.ToString());
            return $"Connection error: {ex.Message}";
        }
    }

    public async Task<string?> DeletePart (long id)
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

        } catch (Exception ex)
        {
            Debug.WriteLine(ex.ToString());
            return $"Connection error: {ex.Message}";
        }
    }
}
