using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CRMSystem.Core.Models;

public class ProposedPart
{
    public ProposedPart(int id, int proposalId, int usedPartId)
    {
        Id = id;
        ProposalId = proposalId;
        UsedPartId = usedPartId;
    }
    public int Id { get; }

    public int ProposalId { get; }

    public int UsedPartId { get; }

    public static (ProposedPart proposedPart, string error) Create(int id, int proposalId, int usedPartId)
    {
        var error = string.Empty;

        if (proposalId <= 0)
            error = "Proposal Id must be positive";

        if (usedPartId <= 0)
            error = "Used part Id must be positive";

        var proposalPart = new ProposedPart(id, proposalId, usedPartId);

        return (proposalPart, error);
    }
                
}
