namespace CRMSystem.DataAccess.Entites;

public class ProposedPartEntity
{
    public int Id { get; set; }

    public int ProposalId { get; set; }

    public int UsedPartId { get; set; }

    public UsedPartEntity? UsedPart { get; set; }

    public WorkProposalEntity? WorkProposal { get; set; }
}
