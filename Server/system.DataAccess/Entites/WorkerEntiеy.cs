namespace CRMSystem.DataAccess.Entites;

public class WorkerEntiеy
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int SpecializationId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Surname { get; set; } = string.Empty;

    public decimal HourlyRate { get; set; }

    public string PhoneNumber { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public UserEntity? User { get; set; } 

    public SpecializationEntity? Specialization { get; set; }

    public ICollection<WorkEntity> Works { get; set; } = new List<WorkEntity>();

    public ICollection<WorkProposalEntity> WorkProposals { get; set; } = new List<WorkProposalEntity>();
}
