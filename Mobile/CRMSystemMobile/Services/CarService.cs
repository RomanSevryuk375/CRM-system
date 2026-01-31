using Shared.Contracts.Car;
using System.Diagnostics;
using System.Net.Http.Json;

namespace CRMSystemMobile.Services;

public class CarService(HttpClient httpClient)
{
    public async Task<List<CarResponse>?> GetMyCars()
    {
        string query = "Page=1&Limit=100&IsDescending=true";
        string url = $"api/Car?{query}";

        try
        {
            var response = await httpClient.GetAsync(url);

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                return null;

            response.EnsureSuccessStatusCode();

            var items = await response.Content.ReadFromJsonAsync<List<CarResponse>>();
            return items;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error getting cars: {ex.Message}");
            return null;
        }
    }
}