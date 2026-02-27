using System.Net.Http.Json;
using CRMSystemMobile.Extensions;
using Shared.Contracts.Skill;
using Shared.Filters;

namespace CRMSystemMobile.Services;

public class SkillService(HttpClient httpClient)
{
    public async Task<List<SkillResponse>?> GetWorkerSkills(int workerId)
    {
        var filter = new SkillFilter(
            WorkerIds: [workerId],
            SpecializationIds: [],
            SortBy: null,
            IsDescending: false
        );

        var (items, _) = await httpClient.GetPagedAsync<SkillResponse>("api/v1/skills", filter);
        return items;
    }

    public async Task<string?> CreateSkill(SkillRequest request)
    {
        try
        {
            var response = await httpClient.PostAsJsonAsync("api/v1/skills", request);
            if (response.IsSuccessStatusCode)
            {
                return null;
            }

            return await response.Content.ReadAsStringAsync();
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    public async Task<string?> DeleteSkill(int id)
    {
        try
        {
            var response = await httpClient.DeleteAsync($"api/v1/skills/{id}");
            if (response.IsSuccessStatusCode)
            {
                return null;
            }

            return await response.Content.ReadAsStringAsync();
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }
}