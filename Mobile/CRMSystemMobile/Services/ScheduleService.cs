using CRMSystemMobile.Extentions;
using Shared.Contracts.Schedule;
using Shared.Filters;
using System.Diagnostics;
using System.Net.Http.Json;

namespace CRMSystemMobile.Services;

public class ScheduleService(HttpClient httpClient, IdentityService identityService)
{
    public async Task<List<ScheduleResponse>?> GetMySchedules()
    {
        try
        {
            var (profileId, _) = await identityService.GetProfileIdAsync();
            if (profileId <= 0) return null;

            var query = $"Page=1&Limit=50&IsDescending=true&SortBy=date&WorkerIds={profileId}";

            string url = $"api/Schedule?{query}";

            var response = await httpClient.GetAsync(url);

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                SecureStorage.Default.Remove("jwt_token");
                await Shell.Current.GoToAsync("//LoginPage");
                return null;
            }

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<ScheduleResponse>>();
            }

            return null;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.ToString());
            return null;
        }
    }
}