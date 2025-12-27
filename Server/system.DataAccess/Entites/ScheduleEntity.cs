namespace CRMSystem.DataAccess.Entites;

public class ScheduleEntity
{
    public int Id { get; set; }
    public int WorkerId { get; set; }
    public int ShiftId { get; set; }
    public DateTime Date { get; set; }

    public WorkerEntity? Worker { get; set; }
    public ShiftEntity? Shift { get; set; }

}
