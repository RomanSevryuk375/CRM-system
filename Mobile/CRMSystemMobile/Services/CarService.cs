using CRMSystemMobile.Extentions;
using Shared.Contracts.Car;
using System.Diagnostics;
using System.Net.Http.Json;

namespace CRMSystemMobile.Services;

public class CarService(HttpClient httpClient, IdentityService identityService)
{
    public async Task<List<CarResponse>?> GetMyCars()
    {
        try
        {
            var (profileId, _) = await identityService.GetProfileIdAsync();

            if (profileId <= 0)
            {
                return null;
            }

            string url = $"api/Car?Page=1&Limit=100&IsDescending=true&OwnerIds={profileId}";

            var response = await httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<CarResponse>>();
            }

            return null;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public async Task<string?> CreateCar(CarRequest request)
    {
        try
        {
            var response = await httpClient.PostAsJsonAsync("api/Car", request);

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
            Debug.WriteLine(ex);
            return $"Ошибка соединения: {ex.Message}";
        }
    }
}