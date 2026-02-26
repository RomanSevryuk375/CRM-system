using System.Diagnostics;
using System.Net.Http.Json;
using System.Text.Json;
using CRMSystemMobile.Extensions.HttpExtensions;

namespace CRMSystemMobile.Extensions
{
    public static class HttpClientExtensions
    {
        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public static async Task<(List<TResponse>?, int TotalCount)> GetPagedAsync<TResponse>(
            this HttpClient httpClient,
            string baseUrl,
            object filter)
        {
            try
            {
                var queryString = BuildQueryExtension.BuildQuery(filter);
                var url = $"{baseUrl}?{queryString}";

                var response = await httpClient.GetAsync(url);

                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    SecureStorage.Default.Remove("jwt_token");
                    MainThread.BeginInvokeOnMainThread(async () => { await Shell.Current.GoToAsync("//LoginPage"); });
                    return (null, 0);
                }

                response.EnsureSuccessStatusCode();

                var totalCount = 0;
                if (response.Headers.TryGetValues("x-total-count", out var values))
                {
                    int.TryParse(values.FirstOrDefault(), out totalCount);
                }

                var items = await response.Content.ReadFromJsonAsync<List<TResponse>>(JsonOptions);

                return (items, totalCount);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error fetching {typeof(TResponse).Name}: {ex}");
                return (null, 0);
            }
        }
    }
}