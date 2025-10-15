namespace CRM_system_backend.Contracts
{
    public record ClientsRequest(
        int UserId,
        string Name,
        string Surname,
        string Email,
        string PhoneNumber
        );
}
