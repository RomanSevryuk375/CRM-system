namespace CRMSystem.Infrastructure
{
    public interface IMyPasswordHasher
    {
        string Generate(string password);
        bool Verify(string password, string passwordHash);
    }
}