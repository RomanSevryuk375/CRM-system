 namespace CRMSystem.Core.ProjectionModels.Order;

public record OrderDocumentModel
(
    long OrderId,
    ClientData ClientData,
    CarLineData CarData,
    List<PartLineData> PartsData,
    List<WorkLineData>  WorkLines,
    DateOnly StartDate,
    DateTime FinishDate,
    List<string?> GuaranteeData,
    List<string> GuaranteeTerms);
    
public record ClientData
(
    string Name,
    string Surname,
    string PhoneNumber);
    
public record CarLineData
(
    string Brand,
    string Model,
    int YearOfManufacture,
    string VinNumber,
    string StateNumber);
    
public record PartLineData
(
    string Name,
    decimal SoldPrice,
    decimal Quantity);
    
public record WorkLineData
(
    string Job,
    decimal TimeSpent,
    decimal HourlyPrice);