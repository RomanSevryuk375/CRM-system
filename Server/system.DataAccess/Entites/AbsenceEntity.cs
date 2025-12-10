using CRMSystem.Core.Enums;

namespace CRMSystem.DataAccess.Entites;

public class AbsenceEntity
{
    public int Id { get; set; }
    public int WorkerId { get; set; }
    public AbsenceTypeEnum TypeId { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly? EndDate { get; set; }

    public WorkerEntity? Worker { get; set; }
    public AbsenceTypeEntity? AbsenceType { get; set; }

}
