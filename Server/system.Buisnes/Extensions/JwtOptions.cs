// Ignore Spelling: Jwt

namespace CRMSystem.Business.Extensions;

public class JwtOptions
{
    public string SecretKey { get; set; } = string.Empty;

    public int ExpiresHours { get; set; }
}