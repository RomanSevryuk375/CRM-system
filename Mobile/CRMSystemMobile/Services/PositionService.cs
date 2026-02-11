using Shared.Contracts.Position;
using Shared.Filters;
using System.Diagnostics;
using System.Net.Http.Json;

namespace CRMSystemMobile.Services;

public class PositionService(HttpClient httpClient)
{
    public async Task<(List<PositionResponse>?, int TotalCount)> GetPositions(PositionFilter filter)
    {
        try
        {
            var query = $"Page={filter.Page}&Limit={filter.Limit}&IsDescending={filter.IsDescending}";

            if (!string.IsNullOrEmpty(filter.SortBy))
            {
                query += $"&SortBy={filter.SortBy}";
            }

            if (filter.PartIds?.Any() == true)
            {
                foreach (var id in filter.PartIds)
                {
                    query += $"&PartIds={id}";
                }
            }

            string url = $"api/v1/positions?{query}";

            var response = await httpClient.GetAsync(url);

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                SecureStorage.Default.Remove("jwt_token");
                await Shell.Current.GoToAsync("//LoginPage");
                return (null, 0);
            }

            response.EnsureSuccessStatusCode();

            int totalCount = 0;
            if (response.Headers.TryGetValues("x-total-count", out var values))
            {
                int.TryParse(values.FirstOrDefault(), out totalCount);
            }

            var items = await response.Content.ReadFromJsonAsync<List<PositionResponse>>();
            return (items, totalCount);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.ToString());
            return (null, 0);
        }
    }
}