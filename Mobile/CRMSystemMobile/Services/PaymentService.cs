using Shared.Contracts.PaymentNote;
using Shared.Filters;
using System.Diagnostics;
using System.Net.Http.Json;

namespace CRMSystemMobile.Services;

public class PaymentService(HttpClient httpClient)
{
    public async Task<(List<PaymentNoteResponse>? items, int TotalCount)> GetMyPayments(PaymentNoteFilter filter)
    {
        var query = $"Page={filter.Page}&Limit={filter.Limit}&IsDescending={filter.IsDescending}";

        if (!string.IsNullOrEmpty(filter.SortBy))
        {
            query += $"&SortBy={filter.SortBy}";
        }

        if (filter.BillIds?.Any() == true)
        {
            foreach (var id in filter.BillIds)
            {
                if (id.HasValue)
                {
                    query += $"&BillIds={id}";
                }
            }
        }

        if (filter.MethodIds?.Any() == true)
        {
            foreach (var id in filter.MethodIds)
            {
                query += $"&MethodIds={id}";
            }
        }

        try
        {
            var response = await httpClient.GetAsync($"api/v1/payment-notes?{query}");

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

            var items = await response.Content.ReadFromJsonAsync<List<PaymentNoteResponse>>();
            return (items, totalCount);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error fetching payments: {ex}");
            return (null, 0);
        }
    }
    public async Task<string?> CreatePayment(PaymentNoteRequest request)
    {
        try
        {
            var response = await httpClient.PostAsJsonAsync("api/v1/payment-notes", request);

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
            return $"Server error: {ex.Message}";
        }
    }
}