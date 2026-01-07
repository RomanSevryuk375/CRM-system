// Ignore Spelling: Hasher

using CRMSystem.Core.Abstractions;

namespace CRMSystem.Business.Extensions;

public class MyPasswordHasher : IMyPasswordHasher
{
    public string Generate(string password) =>
        BCrypt.Net.BCrypt.EnhancedHashPassword(password);

    public bool Verify(string password, string passwordHash) =>
        BCrypt.Net.BCrypt.EnhancedVerify(password, passwordHash);
}