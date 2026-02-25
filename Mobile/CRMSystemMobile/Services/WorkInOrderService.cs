using Shared.Contracts.WorkInOrder;
using Shared.Filters;
using System.Diagnostics;
using System.Net.Http.Json;
using CRMSystemMobile.Extensions;

namespace CRMSystemMobile.Services;

public class WorkInOrderService(HttpClient httpClient)
{
    public async Task<(List<WorkInOrderResponse>?, int TotalCount)> GetWorksInOrder(WorkInOrderFilter filter)
    {
        return await httpClient.GetPagedAsync<WorkInOrderResponse>("api/works-in-order", filter);
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

    public async Task<string?> UpdateWorkInOrder(long id, WorkInOrderUpdateRequest model)
    {
        try
        {
            var response = await httpClient.PutAsJsonAsync($"api/works-in-order/{id}", model);

            if (response.IsSuccessStatusCode)
            {
                return null;
            }

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