namespace CRM.Shared.Abstractions.Abstractions;

public interface IUserContext
{
    bool IsAuthenticated { get; }
    Guid UserId { get; }
}

