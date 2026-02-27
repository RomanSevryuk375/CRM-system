using CRMSystemMobile.Extensions;
using Shared.Contracts.Absence;
using Shared.Filters;

namespace CRMSystemMobile.Services;

public class AbsenceService(HttpClient httpClient)
{
    public async Task<(List<AbsenceResponse>? items, int TotalCount)> GetMyAbsences(AbsenceFilter filter)
    {
        return await httpClient.GetPagedAsync<AbsenceResponse>("api/v1/absences", filter);
    }
}