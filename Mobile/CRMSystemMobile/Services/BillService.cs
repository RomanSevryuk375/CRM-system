using Shared.Contracts.Bill;
using Shared.Filters;
using System.Diagnostics;
using System.Net.Http.Json;
using CRMSystemMobile.Extensions;

namespace CRMSystemMobile.Services;

public class BillService(HttpClient httpClient)
{
    public async Task<(List<BillResponse>? items, int TotalCount)> GetBills(BillFilter filter)
    {
        return await httpClient.GetPagedAsync<BillResponse>("api/v1/bills?", filter);
    }

    public async Task<decimal?> GetBillDebt(long billId)
    {
        try
        {
            var url = $"api/v1/bills/debt/{billId}";

            var response = await httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var content = await response.Content.ReadAsStringAsync();
            if (decimal.TryParse(content, System.Globalization.NumberStyles.Any,
                    System.Globalization.CultureInfo.InvariantCulture, out var debt))
            {
                return debt;
            }

            return null;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error fetching debt: {ex}");
            return null;
        }
    }
}