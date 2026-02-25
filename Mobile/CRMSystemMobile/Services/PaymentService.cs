using Shared.Contracts.PaymentNote;
using Shared.Filters;
using System.Diagnostics;
using System.Net.Http.Json;
using CRMSystemMobile.Extensions;

namespace CRMSystemMobile.Services;

public class PaymentService(HttpClient httpClient)
{
    public async Task<(List<PaymentNoteResponse>? items, int TotalCount)> GetMyPayments(PaymentNoteFilter filter)
    {
        return await httpClient.GetPagedAsync<PaymentNoteResponse>("api/v1/payment-notes", filter);
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