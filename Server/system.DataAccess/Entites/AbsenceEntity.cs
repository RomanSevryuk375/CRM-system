namespace CRMSystem.DataAccess.Entites;

public class AbsenceEntity
{
    public int Id { get; set; }
    public int WorkerId { get; set; }
    public string Type { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public WorkerEntity? Worker { get; set; }

}
