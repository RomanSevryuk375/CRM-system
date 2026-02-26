using System.Collections;
using System.Diagnostics;
using System.Globalization;
using System.Net.Http.Json;
using System.Text.Json;

namespace CRMSystemMobile.Extensions
{
    public static class HttpClientExtensions
    {
        private static readonly JsonSerializerOptions _jsonOptions = new()
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
                var queryString = BuildQueryString(filter);
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

                var items = await response.Content.ReadFromJsonAsync<List<TResponse>>(_jsonOptions);

                return (items, totalCount);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error fetching {typeof(TResponse).Name}: {ex}");
                return (null, 0);
            }
        }

        private static string BuildQueryString<T>(T filter)
        {
            if (filter == null) return string.Empty;

            var properties = filter.GetType().GetProperties();
            var queryParams = new List<string>();

            foreach (var prop in properties)
            {
                var value = prop.GetValue(filter);

                if (value == null) continue;

                switch (value)
                {
                    case string str:
                        if (string.IsNullOrWhiteSpace(str)) continue;
                        queryParams.Add($"{prop.Name}={Uri.EscapeDataString(str)}");
                        break;

                    case IEnumerable list:
                        foreach (var item in list)
                        {
                            if (item == null) continue;

                            var encodedItem = Uri.EscapeDataString(FormatValue(item));
                            queryParams.Add($"{prop.Name}={encodedItem}");
                        }
                        break;

                    default:
                        var encodedValue = Uri.EscapeDataString(FormatValue(value));
                        queryParams.Add($"{prop.Name}={encodedValue}");
                        break;
                }
            }

            return string.Join("&", queryParams);
        }

        private static string FormatValue(object value)
        {
            if (value == null) return string.Empty;

            if (value.GetType().IsEnum)
            {
                return ((int)value).ToString();
            }

            return value switch
            {
                DateTime date => date.ToString("yyyy-MM-ddTHH:mm:ss.fffZ", CultureInfo.InvariantCulture),
                DateOnly dateOnly => dateOnly.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
                IFormattable formattable => formattable.ToString(null, CultureInfo.InvariantCulture),
                _ => value.ToString() ?? string.Empty
            };
        }
    }
}