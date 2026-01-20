namespace CRMSystem.Business.Abstractions;

public interface IUserContext
{
    long UserId { get; }
    int RoleId { get; }
    long ProfileId { get; }
}
