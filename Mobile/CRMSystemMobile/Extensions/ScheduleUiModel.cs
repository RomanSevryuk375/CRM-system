using Shared.Contracts.Schedule;

namespace CRMSystemMobile.Extensions;

public class ScheduleUiModel
{
    public int Id { get; set; }
    public DateTime DateTime { get; set; }
    public string ShiftName { get; set; }
    public string TimeRange { get; set; }

    public ScheduleUiModel(ScheduleResponse source, string timeRange)
    {
        Id = source.Id;
        DateTime = source.DateTime;
        ShiftName = source.Shift;
        TimeRange = timeRange;
    }
}