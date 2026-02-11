using Shared.Contracts.Worker;
using System.Net.Http.Json;

namespace CRMSystemMobile.Services;

public class WorkerService(HttpClient httpClient)
{
    public async Task<WorkerResponse?> GetWorkerById(int id)
    {
        try
        {
            var response = await httpClient.GetAsync($"api/v1/workers/{id}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<WorkerResponse>();
            }
            return null;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex);
            return null;
        }
    }

    public async Task<bool> UpdateWorker(int id, WorkerUpdateRequest request)
    {
        try
        {
            var response = await httpClient.PutAsJsonAsync($"api/v1/workers/{id}", request);
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex);
            return false;
        }
    }
}