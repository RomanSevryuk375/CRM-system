// Ignore Spelling: Jwt

using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels.User;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CRMSystem.Business.Extensions;

public class JwtProvider : IJwtProvider
{
    private readonly JwtOptions _options;

    public JwtProvider(IOptions<JwtOptions> options) => _options = options.Value;
    public string GenerateToken(UserItem user)
    {
        Claim[] claims = [ new("userId", user.Id.ToString()),
                           new("userRoleId", user.RoleId.ToString())];

        var singinCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey)),
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            claims: claims,
            signingCredentials: singinCredentials,
            expires: DateTime.UtcNow.AddHours(_options.ExpiresHours)
            );

        var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

        return tokenValue.ToString();
    }
}
