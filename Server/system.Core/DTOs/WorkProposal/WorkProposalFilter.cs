using CRMSystem.Core.Enums;

namespace CRMSystem.Core.DTOs.WorkProposal;

public record WorkProposalFilter
(
    IEnumerable<long> orderIds,
    IEnumerable<long> jobIds,
    IEnumerable<long> workerIds,
    IEnumerable<int> statusIds,
    string? SortBy,
    int Page,
    int Limit,
    bool isDescending
);
