using Shared.Contracts.Client;
using System.Net.Http.Json;
using System.Text.Json;

public record RegistrationResult(bool Success, string? ErrorMessage);

public class RegistrationService
{
    private readonly HttpClient _httpClient;

    public RegistrationService(HttpClient httpClient) => _httpClient = httpClient;

    public async Task<RegistrationResult> RegisterUser(ClientRegisterRequest request)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("api/v1/clients/user", request);
            var content = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                return new RegistrationResult(true, null);
            }

            try
            {
                using var doc = JsonDocument.Parse(content);
                var root = doc.RootElement;

                if (root.TryGetProperty("errors", out var errorsEl))
                {
                    var messages = new List<string>();
                    foreach (var prop in errorsEl.EnumerateObject())
                    {
                        if (prop.Value.ValueKind == JsonValueKind.Array)
                        {
                            foreach (var item in prop.Value.EnumerateArray())
                                messages.Add($"{prop.Name}: {item.GetString()}");
                        }
                        else
                        {
                            messages.Add($"{prop.Name}: {prop.Value.GetString()}");
                        }
                    }
                    return new RegistrationResult(false, string.Join(Environment.NewLine, messages));
                }

                if (root.TryGetProperty("message", out var msgEl))
                {
                    return new RegistrationResult(false, msgEl.GetString());
                }

                if (root.ValueKind == JsonValueKind.String)
                {
                    return new RegistrationResult(false, root.GetString());
                }
            }
            catch
            {
                // If not JSON, return the raw content.
            }

            return new RegistrationResult(false, string.IsNullOrWhiteSpace(content) ? response.ReasonPhrase : content);
        }
        catch (HttpRequestException httpEx)
        {
            return new RegistrationResult(false, $"Сетевая ошибка: {httpEx.Message}");
        }
        catch (Exception ex)
        {
            return new RegistrationResult(false, $"Ошибка: {ex.Message}");
        }
    }
}
