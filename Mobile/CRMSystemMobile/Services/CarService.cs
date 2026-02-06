using CRMSystemMobile.Extentions;
using Shared.Contracts.Car;
using Shared.Filters;
using System.Diagnostics;
using System.Net.Http.Json;

namespace CRMSystemMobile.Services;

public class CarService(HttpClient httpClient)
{
    public async Task<(List<CarResponse>? items, int TotalCount)> GetCars(CarFilter filter)
    {
        try
        {
            var query = $"Page={filter.Page}&Limit={filter.Limit}&IsDescending={filter.IsDescending}";

            if (profileId <= 0)
            {
                return null;
            }

            string url = $"api/Car?Page=1&Limit=100&IsDescending=true&OwnerIds={profileId}";

            var response = await httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                int.TryParse(values.FirstOrDefault(), out totalCount);
            }

            var items = await response.Content.ReadFromJsonAsync<List<CarResponse>>();
            return (items, totalCount);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.ToString());
            return (null, 0);
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