using Shared.Contracts.Order;
using Shared.Filters;
using System.Net.Http.Json;
using CRMSystemMobile.Extensions;

namespace CRMSystemMobile.Services;

public class OrderService(HttpClient httpClient)
{
    public async Task<(List<OrderResponse>? items, int TotalCount)> GetOrders(OrderFilter filter)
    {
        return await httpClient.GetPagedAsync<OrderResponse>("api/v1/orders", filter);
    }
    
    public async Task<byte[]?> GetOrderPdf(long orderId)
    {
        try
        {
            var response = await httpClient.GetAsync($"api/v1/orders/{orderId}/pdf");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            return await response.Content.ReadAsByteArrayAsync();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error downloading PDF: {ex.Message}");
            return null;
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