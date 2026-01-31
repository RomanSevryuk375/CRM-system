using Shared.Contracts.Client;
using System.Net.Http.Json;

namespace CRMSystemMobile.Services;

public class ClientService(HttpClient httpClient)
{
    public async Task<ClientsResponse?> GetClientById(long id)
    {
        try
        {
            var response = await httpClient.GetAsync($"api/Client/{id}");
            var json = await response.Content.ReadAsStringAsync();

            System.Diagnostics.Debug.WriteLine($"RAW JSON: {json}");

            return await response.Content.ReadFromJsonAsync<ClientsResponse>();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex);
            return null;
        }
    }

    public async Task<bool> UpdateClient(long id, ClientUpdateRequest request)
    {
        try
        {
            var response = await httpClient.PutAsJsonAsync($"api/Client/{id}", request);
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex);
            return false;
        }
    }
}