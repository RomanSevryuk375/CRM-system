namespace Shared.Contracts;

public record PaymentMethodResponse
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
};
