using CRMSystemMobile.Extentions;
using Shared.Contracts.Login;
using System.Diagnostics;
using System.Text;
using System.Text.Json;

namespace CRMSystemMobile.Services;

public class LoginService
{
    private readonly HttpClient _httpClient;

    public LoginService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<LoginResponse?> LoginUser(LoginRequest request)
    {
        string url = $"{ApiConfig.BaseUrl}/User/login";

        try
        {
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var json = JsonSerializer.Serialize(request, options);
            var context = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(url, context);

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var deserializeResponse = JsonSerializer.Deserialize<LoginResponse>(responseString, options);

                if (deserializeResponse?.Token != null)
                    await SecureStorage.Default.SetAsync("jwt_token", deserializeResponse.Token);

                return deserializeResponse;
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
