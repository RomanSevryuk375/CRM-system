namespace CRMSystem.DataAccess.Entites;

public class SpecializationEntity
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public ICollection<WorkerEntiеy> Workers { get; set; } = new List<WorkerEntiеy>(); // спорный момент посмотрю завтра 
}
