using CRMSystem.Core.Validation;

namespace CRMSystem.Core.Models;

public class Skill
{
    private Skill(int id, int workerId, int specializationId)
    {
        Id = id;
        WorkerId = workerId;
        SpecializationId = specializationId;
    }

    public int Id { get; }
    public int WorkerId { get; }
    public int SpecializationId { get; }

    public static (Skill? skill, List<string> errors) Create(int id, int workerId, int specializationId)
    {
        var errors = new List<string>();

        var idError = DomainValidator.ValidateId(id, "id");
        if (!string.IsNullOrEmpty(idError)) errors.Add(idError);

        var workerIdError = DomainValidator.ValidateId(workerId, "workerId");
        if (!string.IsNullOrEmpty(workerIdError)) errors.Add(workerIdError);

        var specIdError = DomainValidator.ValidateId(specializationId, "specId");
        if (!string.IsNullOrEmpty(specIdError)) errors.Add(specIdError);

        if (errors.Any())
            return (null, errors);

        var skill = new Skill(id, workerId, specializationId);

        return (skill, new List<string>());
    }
}
