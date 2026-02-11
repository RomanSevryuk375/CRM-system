using Shared.Contracts.PartSet;
using Shared.Contracts.WorkInOrder;
using Shared.Filters;
using System.Diagnostics;
using System.Net.Http.Json;

namespace CRMSystemMobile.Services;

public class WorkInOrderService(HttpClient httpClient)
{
    public async Task<(List<WorkInOrderResponse>?, int TotalCount)> GetWorksInOrder(WorkInOrderFilter filter)
    {
        try
        {
            var query = $"Page={filter.Page}&Limit={filter.Limit}&IsDescending={filter.IsDescending}";

            if (!string.IsNullOrEmpty(filter.SortBy))
            {
                query += $"&SortBy={filter.SortBy}";
            }

            if (filter.JobIds?.Any() == true)
            {
                foreach (var id in filter.JobIds)
                {
                    query += $"&JobIds={id}";
                }
            }

            if (filter.StatusIds?.Any() == true)
            {
                foreach (var id in filter.StatusIds)
                {
                    query += $"&StatusIds={id}";
                }
            }

            if (filter.OrderIds?.Any() == true)
            {
                foreach (var id in filter.OrderIds)
                {
                    query += $"&OrderIds={id}";
                }
            }

            if (filter.WorkerIds?.Any() == true)
            {
                foreach (var id in filter.WorkerIds)
                {
                    query += $"&WorkerIds={id}";
                }
            }

            string url = $"api/works-in-order?{query}";

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

            var items = await response.Content.ReadFromJsonAsync<List<WorkInOrderResponse>>();
            return (items, totalCount);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.ToString());
            return (null, 0);
        }
    }

    public async Task<string?> AddWorkToOrder(WorkInOrderRequest request)
    {
        try
        {
            var response = await httpClient.PostAsJsonAsync("api/works-in-order", request);

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
            return null;
        }
    }

    public async Task<string?> UpdateWorkInOrder(long id, WorkInOrderUpdateRequest model)
    {
        try
        {
            var response = await httpClient.PutAsJsonAsync($"api/works-in-order/{id}", model);

            if (response.IsSuccessStatusCode) return null;

            var error = await response.Content.ReadAsStringAsync();
            return error;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    public async Task<string?> DeleteWorkInOrder(long id)
    {
        try
        {
            var response = await httpClient.DeleteAsync($"api/works-in-order/{id}");

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
            return null;
        }
    }
}
