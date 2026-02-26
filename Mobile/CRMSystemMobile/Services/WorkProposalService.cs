using Shared.Contracts.WorkProposal;
using Shared.Enums;
using Shared.Filters;
using System.Diagnostics;
using System.Net.Http.Json;
using CRMSystemMobile.Extensions;

namespace CRMSystemMobile.Services;

public class WorkProposalService(HttpClient httpClient)
{
    public async Task<(List<WorkProposalResponse>?, int TotalCount)> GetWorkProposals(WorkProposalFilter filter)
    {
        return await httpClient.GetPagedAsync<WorkProposalResponse>("api/v1/work-proposals", filter);
    }

    public async Task<string?> CreateWorkProposal(WorkProposalRequest request)
    {
        try
        {
            var response = await httpClient.PostAsJsonAsync("api/v1/work-proposals", request);

            if (response.IsSuccessStatusCode)
            {
                return null;
            }

            var errorContent = await response.Content.ReadAsStringAsync();

            return string.IsNullOrWhiteSpace(errorContent)
                ? $"Server error: {response.StatusCode}"
                : errorContent.Trim('"');
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            return $"Server error: {ex.Message}";
        }
    }

    public async Task<string?> DeleteWorkProposal(long id)
    {
        try
        {
            var response = await httpClient.DeleteAsync($"api/v1/work-proposals/{id}");

            if (response.IsSuccessStatusCode)
            {
                return null;
            }

            var errorContent = await response.Content.ReadAsStringAsync();

            return string.IsNullOrWhiteSpace(errorContent)
                ? $"Server error: {response.StatusCode}"
                : errorContent.Trim('"');
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            return $"Server error: {ex.Message}";
        }
    }

    public async Task<string?> RejectWorkProposal(long id)
    {
        var request = new ProposalStatusRequest
        {
            Status = ProposalStatusEnum.Rejected
        };

        return await SendStatusUpdate(id, request);
    }

    public async Task<string?> AcceptWorkProposal(long id)
    {
        var request = new ProposalStatusRequest
        {
            Status = ProposalStatusEnum.Accepted
        };

        return await SendStatusUpdate(id, request);
    }

    private async Task<string?> SendStatusUpdate(long id, ProposalStatusRequest request)
    {
        try
        {
            var response = await httpClient.PutAsJsonAsync($"api/v1/work-proposals/{id}/status", request);

            if (response.IsSuccessStatusCode)
            {
                return null;
            }

            var errorContent = await response.Content.ReadAsStringAsync();

            return string.IsNullOrWhiteSpace(errorContent)
                ? $"Server error: {response.StatusCode}"
                : errorContent.Trim('"');
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            return $"Client error: {ex.Message}";
        }
    }
}