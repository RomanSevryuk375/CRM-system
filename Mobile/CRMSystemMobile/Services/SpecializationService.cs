using System.Net.Http.Json;
using Shared.Contracts.Specialization;

namespace CRMSystemMobile.Services;

public class SpecializationService(HttpClient httpClient)
{
    public async Task<List<SpecializationResponse>?> GetAllSpecializations()
    {
        try
        {
            var response = await httpClient.GetAsync($"api/v1/specializations");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<SpecializationResponse>>();
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex);
            return null;
        }

        return null;
    }
}