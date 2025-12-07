namespace CRMSystem.DataAccess.Entites;

public class WorkerEntity
{
    public int Id { get; set; }
    public long UserId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public decimal HourlyRate { get; set; }
    public string PhoneNumber { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    public UserEntity? User { get; set; }
    public ICollection<WorkInOrderEntity> WorksInOrder { get; set; } = new List<WorkInOrderEntity>();
    public ICollection<SkillEntity> Skills { get; set; } = new List<SkillEntity>();
    public ICollection<AbsenceEntity> Absences { get; set; } = new List<AbsenceEntity>();
    public ICollection<AttachmentEntity> Attachments { get; set; } = new List<AttachmentEntity>();
    public ICollection<ScheduleEntity> Schedules { get; set; } = new List<ScheduleEntity>();
    public ICollection<WorkProposalEntity> WorkProposals { get; set; } = new List<WorkProposalEntity>();
} 
