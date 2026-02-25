using Shared.Contracts.Car;
using Shared.Filters;
using System.Diagnostics;
using System.Net.Http.Json;
using CRMSystemMobile.Extensions;

namespace CRMSystemMobile.Services;

public class CarService(HttpClient httpClient)
{
    public async Task<(List<CarResponse>? items, int TotalCount)> GetCars(CarFilter filter)
    {
        return await httpClient.GetPagedAsync<CarResponse>("api/v1/cars", filter);
    }

    public async Task<string?> CreateCar(CarRequest request)
    {
        try
        {
            var response = await httpClient.PostAsJsonAsync("api/v1/cars", request);

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
            Debug.WriteLine(ex);
            return $"Server error: {ex.Message}";
        }
    }
}