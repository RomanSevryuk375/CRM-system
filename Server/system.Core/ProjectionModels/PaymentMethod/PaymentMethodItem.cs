namespace CRMSystem.Core.ProjectionModels.PaymentMethod;

public record PaymentMethodItem
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
};
