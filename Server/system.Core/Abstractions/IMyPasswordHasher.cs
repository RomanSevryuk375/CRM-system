// Ignore Spelling: Hasher

namespace CRMSystem.Core.Abstractions;

public interface IMyPasswordHasher
{
    string Generate(string password);
    bool Verify(string password, string passwordHash);
}