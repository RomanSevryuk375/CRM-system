using CRMSystem.Core.DTOs.User;
using CRMSystem.Core.Models;

namespace CRMSystem.Buisnes.Extensions;

public interface IJwtProvider
{
    string GenerateToken(UserItem user);
}