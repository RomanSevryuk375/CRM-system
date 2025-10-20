using CRMSystem.Core.Models;

namespace CRMSystem.Buisnes.Extensions;

public interface IJwtProvider
{
    string GenerateToken(User user);
}