using Shared.Contracts.Order;
using Shared.Filters;
using System.Diagnostics;
using System.Net.Http.Json;

namespace CRMSystemMobile.Services;

public class OrderService(HttpClient httpClient)
{
    public async Task<(List<OrderResponse>? items, int TotalCount)> GetOrders(OrderFilter orderFilter)
    {
        var query = $"Page={orderFilter.Page}&Limit={orderFilter.Limit}&IsDescending={orderFilter.IsDescending}";

        if (!string.IsNullOrEmpty(orderFilter.SortBy))
        {
            query += $"&SortBy={orderFilter.SortBy}";
        }

        if (orderFilter.StatusIds?.Any() == true)
        {
            query = orderFilter.StatusIds.Aggregate(query, (current, id) => current + $"&StatusIds={id}");
        }

        if (orderFilter.CarIds?.Any() == true)
        {
            query = orderFilter.CarIds.Aggregate(query, (current, id) => current + $"&CarIds={id}");
        }

        if (orderFilter.ClientIds?.Any() == true)
        {
            query = orderFilter.ClientIds.Aggregate(query, (current, id) => current + $"&ClientIds={id}");
        }

        if (orderFilter.PriorityIds?.Any() == true)
        {
            query = orderFilter.PriorityIds.Aggregate(query, (current, id) => current + $"&PriorityIds={id}");
        }

        if (orderFilter.WorkerIds?.Any() == true)
        {
            query = orderFilter.WorkerIds.Aggregate(query, (current, id) => current + $"&WorkerIds={id}");
        }

        try
        {
            var response = await httpClient.GetAsync($"api/v1/orders?{query}");

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
                totalCount = int.Parse(values.First());
            }

            await response.Content.ReadAsStringAsync();
            var items = await response.Content.ReadFromJsonAsync<List<OrderResponse>>();
            return (items, totalCount);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.ToString());
            return (null, 0);
        }
    }

    public async Task<string?> CreateOrder(OrderRequest request)
    {
        try
        {
            var response = await httpClient.PostAsJsonAsync("api/v1/orders", request);

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
            return $"Server error: {ex.Message}";
        }
    }
}