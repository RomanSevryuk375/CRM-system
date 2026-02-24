using Shared.Contracts.Login;
using System.Diagnostics;
using System.Net.Http.Json;

namespace CRMSystemMobile.Services;

public class LoginService(HttpClient httpClient)
{
    public async Task<LoginResponse?> LoginUser(LoginRequest request)
    {
        var url = $"api/v1/users/login";

        try
        {
            var response = await httpClient.PostAsJsonAsync(url, request);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var result = await response.Content.ReadFromJsonAsync<LoginResponse>();

            if (result?.Token != null)
            {
                await SecureStorage.Default.SetAsync("jwt_token", result.Token);
            }

            return result;

        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.ToString());
            return null;
        }
    }
}