namespace CRMSystem.DataAccess.Entites;

public class AbsenceTypeEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public ICollection<AbsenceEntity> Absences { get; set; } = new HashSet<AbsenceEntity>();
}
