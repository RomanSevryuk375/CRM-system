using CRMSystem.Core.Constants;
using CRMSystem.Core.Validation;

namespace CRMSystem.Core.Models;

public class Acceptance
{
    private Acceptance(long id, long orderId, int workerId, DateTime createdAt, int mileage,
        int fuelLevel, string? externalDefects, string? internalDefects, bool? clientSign, bool? workerSign)
    {
        Id = id;
        OrderId = orderId;
        WorkerId = workerId;
        CreateAt = createdAt; 
        Mileage = mileage; 
        FuelLevel = fuelLevel; 
        InternalDefects = internalDefects;
        ExternalDefects = externalDefects;
        ClientSign = clientSign; 
        WorkerSign = workerSign;
    }
    public long Id { get; }
    public long OrderId { get; }
    public int WorkerId { get; }
    public DateTime CreateAt { get; }
    public int Mileage { get; }
    public int FuelLevel { get; }
    public string? ExternalDefects { get; }
    public string? InternalDefects { get; }
    public bool? ClientSign { get; }
    public bool? WorkerSign { get; }

    public static (Acceptance? acceptance, List<string>? error) Create (long id, long orderId, int workerId, DateTime createdAt, int mileage,
        int fuelLevel, string? externalDefects, string? internalDefects, bool? clientSign, bool? workerSign)
    {
        var errors = new List<string>();

        var idError = DomainValidator.ValidateId(id, "Absence ID");
        if (!string.IsNullOrEmpty(idError)) errors.Add(idError);

        var orderIdError = DomainValidator.ValidateId(orderId, "Absence ID");
        if (!string.IsNullOrEmpty(orderIdError)) errors.Add(orderIdError);

        var workerIdError = DomainValidator.ValidateId(workerId, "Absence ID");
        if (!string.IsNullOrEmpty(workerIdError)) errors.Add(workerIdError);

        var mileageError = DomainValidator.ValidateId(mileage, "Absence ID");
        if (!string.IsNullOrEmpty(mileageError)) errors.Add(mileageError);

        var fuelLevelError = DomainValidator.ValidateId(fuelLevel, "Absence ID");
        if (!string.IsNullOrEmpty(fuelLevelError)) errors.Add(fuelLevelError);

        var createAtError = DomainValidator.ValidateDate(createdAt, "createdAt");
        if (!string.IsNullOrEmpty(createAtError)) errors.Add(createAtError);

        var externalDefectsError = DomainValidator.ValidateString(externalDefects, ValidationConstants.MAX_DESCRIPTION_LENGTH, "externalDefects");
        if (!string.IsNullOrEmpty(externalDefectsError)) errors.Add(externalDefectsError);

        var internalDefectsError = DomainValidator.ValidateString(internalDefects, ValidationConstants.MAX_DESCRIPTION_LENGTH, "externalDefects");
        if (!string.IsNullOrEmpty(internalDefectsError)) errors.Add(internalDefectsError);

        if (errors.Any())
            return (null, errors);

        var acceptance = new Acceptance(
            id,
            orderId, 
            workerId, 
            createdAt, 
            mileage, 
            fuelLevel, 
            externalDefects, 
            internalDefects, 
            clientSign, 
            workerSign
        );

        return (acceptance, new List<string>());
    }
}
