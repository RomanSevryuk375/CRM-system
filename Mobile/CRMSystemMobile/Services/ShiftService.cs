using Shared.Contracts.Shift;
using System.Net.Http.Json;
using System.Diagnostics;

namespace CRMSystemMobile.Services;

public class ShiftService(HttpClient httpClient)
{
    public async Task<List<ShiftResponse>?> GetAllShifts()
    {
        try
        {
            var response = await httpClient.GetFromJsonAsync<List<ShiftResponse>>("api/v1/shifts");
            return response;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"ShiftService Error: {ex.Message}");
            return [];
        }
    }
}