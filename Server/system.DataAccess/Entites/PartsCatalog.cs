namespace CRMSystem.DataAccess.Entites;

public class PartsCatalog
{
    public int Id { get; set; }

    public int CategoryId { get; set; }

    public string OEMArticle { get; set; } = string.Empty;

    public string ManufacturerArticle { get; set; } = string.Empty;

    public string InternalArticle { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string Name {  get; set; } = string.Empty;

    public string Manufacturer { get; set; } = string.Empty;

    public string Applicability { get; set; } = string.Empty;
}
