using Shared.Contracts.PartSet;
using Shared.Filters;
using System.Diagnostics;
using System.Net.Http.Json;

namespace CRMSystemMobile.Services;

public class PartSetService(HttpClient httpClient)
{
    public async Task<(List<PartSetResponse>?, int TotalCount)> GetPartSets(PartSetFilter filter)
    {
        try
        {
            var query = $"Page={filter.Page}&Limit={filter.Limit}&IsDescending={filter.IsDescending}";

            if (!string.IsNullOrWhiteSpace(filter.SortBy))
            {
                query += $"&SortBy={filter.SortBy}";
            }

            if (filter.OrderIds?.Any() == true)
            {
                query = filter.OrderIds.Aggregate(query, (current, id) => current + $"&OrderIds={id}");
            }

            if (filter.PositionIds?.Any() == true)
            {
                query = filter.PositionIds.Aggregate(query, (current, id) => current + $"&PositionIds={id}");
            }

            if (filter.ProposalIds?.Any() == true)
            {
                query = filter.ProposalIds.Aggregate(query, (current, id) => current + $"&ProposalIds={id}");
            }

            var url = $"api/v1/part-sets?{query}";

            var response = await httpClient.GetAsync(url);

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                SecureStorage.Default.Remove("jwt_token");
                await Shell.Current.GoToAsync("//LoginPage");
                return (null, 0);
            }

            response.EnsureSuccessStatusCode();

            var totalCount = 0;
            if (response.Headers.TryGetValues("x-total-count", out var values))
            {
                int.TryParse(values.FirstOrDefault(), out totalCount);
            }

            var items = await response.Content.ReadFromJsonAsync<List<PartSetResponse>>();

            return (items, totalCount);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.ToString());
            return (null, 0);
        }
    }

    public async Task<List<PartSetResponse>?> GetPartsByOrder(long orderId)
    {
        try
        {
            var response = await httpClient.GetAsync($"order/{orderId}");
            // Если не сработает, попробуйте: $"api/PartSet/order/{orderId}" или просто $"order/{orderId}" 

            if (!response.IsSuccessStatusCode) return null;
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