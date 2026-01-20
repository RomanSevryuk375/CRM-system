// Ignore Spelling: Jwt

using CRMSystem.Core.ProjectionModels.User;

namespace CRMSystem.Core.Abstractions;

public interface IJwtProvider
{
    string GenerateToken(UserItem user, long profileId);
}