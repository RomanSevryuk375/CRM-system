namespace CRM.Billing.Api.DTOs;

public sealed record UpdateExpenseRequest(
    DateOnly Date,
    string Category,
    string? Description);