namespace CRMSystem.DataAccess.Entities;

public class ShiftEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public TimeOnly StartAt { get; set; }
    public TimeOnly EndAt { get; set; }

    public ICollection<ScheduleEntity>? Schedules { get; set; }
}
