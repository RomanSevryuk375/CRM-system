using CRMSystemMobile.Extentions;
using Shared.Contracts.Bill;
using Shared.Filters;
using System.Diagnostics;
using System.Net.Http.Json;
using Xamarin.Google.Crypto.Tink.Shaded.Protobuf;

namespace CRMSystemMobile.Services;

public class BillService(HttpClient httpClient)
{
    public async Task<(List<BillResponse>? items, int TotalCount)> GetBills(BillFilter filter)
    {
        try
        {
            var query = $"Page={filter.Page}&Limit={filter.Limit}&IsDescending={filter.IsDescending}";

            if (!string.IsNullOrEmpty(filter.SortBy))
            {
                query += $"&SortBy={filter.SortBy}";
            }

            if (filter.OrderIds?.Any() == true)
            {
                foreach (var id in filter.OrderIds)
                {
                    query += $"&OrderIds={id}";
                }
            }

            string url = $"api/Bill?{query}";

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

            var items = await response.Content.ReadFromJsonAsync<List<BillResponse>>();
            return (items, totalCount);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.ToString());
            return (null, 0);
        }
    }
}