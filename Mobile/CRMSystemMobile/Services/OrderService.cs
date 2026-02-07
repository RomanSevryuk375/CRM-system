using CRMSystemMobile.Extentions;
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
            foreach (var id in orderFilter.StatusIds)
            {
                query += $"&StatusIds={id}";
            }
        }

        if (orderFilter.CarIds?.Any() == true)
        {
            foreach (var id in orderFilter.CarIds)
            {
                query += $"&CarIds={id}";
            }
        }

        if (orderFilter.ClientIds?.Any() == true)
        {
            foreach (var id in orderFilter.ClientIds)
            {
                query += $"&ClientIds={id}";
            }
        }

        if (orderFilter.PriorityIds?.Any() == true)
        {
            foreach (var id in orderFilter.PriorityIds)
            {
                query += $"&PriorityIds={id}";
            }
        }

        if (orderFilter.PriorityIds?.Any() == true)
        {
            foreach (var id in orderFilter.PriorityIds)
            {
                query += $"&PriorityIds={id}";
            }
        }

        if (orderFilter.WorkerIds?.Any() == true)
        {
            foreach (var id in orderFilter.WorkerIds)
            {
                query += $"&WorkerIds={id}";
            }
        }

        try
        {
            var response = await httpClient.GetAsync($"api/Order?{query}");

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
                totalCount = int.Parse(values.First());
            }

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
            var response = await httpClient.PostAsJsonAsync("api/Order", request);

            if (response.IsSuccessStatusCode)
            {
                return null;
            }

            var errorContent = await response.Content.ReadAsStringAsync();
            if (string.IsNullOrWhiteSpace(errorContent))
            {
                return $"Ошибка сервера: {response.StatusCode}";
            }

            return errorContent.Trim('"');
        }
        catch (Exception ex)
        {
            return $"Ошибка соединения: {ex.Message}";
        }
    }
}
