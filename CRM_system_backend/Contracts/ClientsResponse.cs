namespace CRM_system_backend.Contracts
{
    public record ClientsResponse(
        int Id,
        int User_Id,
        string Name,
        string Surname,
        string Email,
        string PhoneNumber
        );
}
