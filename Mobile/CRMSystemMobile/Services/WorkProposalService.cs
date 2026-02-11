using Shared.Contracts.WorkProposal;
using Shared.Filters;
using System.Diagnostics;
using System.Net.Http.Json;

namespace CRMSystemMobile.Services;

public class WorkProposalService(HttpClient httpClient)
{
    public async Task<(List<WorkProposalResponse>?, int TotalCount)> GetWorkProposals(WorkProposalFilter filter)
    {
        try
        {
            var query = $"Page={filter.Page}&Limit={filter.Limit}&IsDescending={filter.IsDescending}";

            if (!string.IsNullOrEmpty(filter.SortBy))
            {
                query += $"&SortBy={filter.SortBy}";
            }

            if (filter.StatusIds?.Any() == true)
            {
                foreach (var id in filter.StatusIds)
                {
                    query += $"&StatusIds={id}";
                }
            }

            if (filter.JobIds?.Any() == true)
            {
                foreach (var id in filter.JobIds)
                {
                    query += $"&JobIds={id}";
                }
            }

            if (filter.WorkerIds?.Any() == true)
            {
                foreach (var id in filter.WorkerIds)
                {
                    query += $"&WorkerIds={id}";
                }
            }

            if (filter.OrderIds?.Any() == true)
            {
                foreach (var id in filter.OrderIds)
                {
                    query += $"&OrderIds={id}";
                }
            }

            string url = $"api/v1/work-proposals?{query}";

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

            var items = await response.Content.ReadFromJsonAsync<List<WorkProposalResponse>>();
            return (items, totalCount);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.ToString());
            return (null, 0);
        }
    }

    public async Task<List<WorkProposalResponse>?> GetProposalsByOrder(long orderId)
    {
        try
        {
            // Используем фильтр для получения предложений по ID заказа
            string query = $"OrderIds={orderId}&Page=1&Limit=100";
            var response = await httpClient.GetAsync($"api/WorkProposal?{query}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<WorkProposalResponse>>();
            }
            return [];
        }
        catch
        {
            return [];
        }
    }

    public async Task<string?> CreateWorkPropsal(WorkProposalRequest request)
    {
        try
        {
            var response = await httpClient.PostAsJsonAsync("api/v1/work-proposals", request);

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
            Debug.WriteLine(ex);
            return $"Server error: {ex.Message}";
        }
    }

    public async Task<string?> DeleteWorkPropsal(long id)
    {
        try
        {
            var response = await httpClient.DeleteAsync($"api/v1/work-proposals/{id}");

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
            Debug.WriteLine(ex);
            return $"Server error: {ex.Message}";
        }
    }

    public async Task<string?> RejectWorkPropsal(long id)
    {
        try
        {
            var response = await httpClient.PutAsync($"api/WorkProposal/{id}/reject", null);

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
            Debug.WriteLine(ex);
            return $"Server error: {ex.Message}";
        }
    }

    public async Task<string?> AcceptWorkPropsal(long id)
    {
        try
        {
            var response = await httpClient.PutAsync($"api/WorkProposal/{id}/accept", null);

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
            Debug.WriteLine(ex);
            return $"Server error: {ex.Message}";
        }
    }
}
