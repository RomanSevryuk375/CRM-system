namespace CRMSystem.DataAccess.Entites;

public class SpecializationEntity
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public ICollection<WorkerEntity> Workers { get; set; } = new List<WorkerEntity>(); // спорный момент посмотрю завтра 
}
