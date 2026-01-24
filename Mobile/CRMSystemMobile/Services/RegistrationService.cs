using CRMSystemMobile.Extentions;
using Shared.Contracts.Client;
using System.Diagnostics;
using System.Net.Http.Json;

namespace CRMSystemMobile.Services;

public class RegistrationService(HttpClient httpClient)
{
    public async Task<bool> RegisterUser(ClientRegisterRequest request)
    {
        string url = $"{ApiConfig.BaseUrl}/Client/with-user";

        try
        {
            var response = await httpClient.PostAsJsonAsync(url, request);

            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            return false;
        }
    }
}
