namespace CRMSystem.DataAccess.Entites;

public class CarEntity
{
    public int Id { get; set; }
    public int OwnerId { get; set; }
    public string Brand { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public int YearOfManufacture { get; set; }
    public string VinNumber { get; set; } = string.Empty;
    public string StateNumber { get; set; } = string.Empty;
    public int Mileage { get; set; }


}
