using CRMSystem.Core.DTOs.User;
using CRMSystem.Core.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CRMSystem.Buisnes.Extensions;

public class JwtProvider : IJwtProvider
{
    private readonly JwtOptions _options;

    public JwtProvider(IOptions<JwtOptions> options) => _options = options.Value;
    public string GenerateToken(UserItem user)
    {
        Claim[] claims = { new("userId", user.Id.ToString()),
                           new("userRoleId", user.RoleId.ToString())};

        var singinCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey)),
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            claims: claims,
            signingCredentials: singinCredentials,
            expires: DateTime.UtcNow.AddHours(_options.ExpitesHours)
            );

        var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

        return tokenValue.ToString();
    }
}
