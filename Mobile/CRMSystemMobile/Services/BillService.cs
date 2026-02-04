using CRMSystemMobile.Extentions;
using Shared.Contracts.Bill;
using System.Net.Http.Json;

namespace CRMSystemMobile.Services;

public class BillService(HttpClient httpClient, IdentityService identityService)
{
    public async Task<List<BillResponse>?> GetMyBills()
    {
        try
        {
            var (profileId, _) = await identityService.GetProfileIdAsync();

            if (profileId <= 0) return null;

            string url = $"api/Bill?Page=1&Limit=100&IsDescending=true&ClientIds={profileId}";

            var response = await httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<BillResponse>>();
            }

            return null;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex);
            return null;
        }
    }
}